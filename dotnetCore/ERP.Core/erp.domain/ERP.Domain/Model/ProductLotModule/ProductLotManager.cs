using ERP.Domain.Context;
using ERP.Domain.Model.OrgModule;
using ERP.Domain.Model.ProductModule;
using ERP.Domain.Model.PurchaseInStockModule;
using ERP.Domain.Model.StockBillModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.ProductLotModule
{
    /// <summary>
    /// 产品批次管理对象：通过该对象加减产品批次的库存
    /// </summary>
    public class ProductLotManager
    {
        // private ERPContext context;
        private List<ProductLot> productLots;
        /// <summary>
        /// 存储产品结存Num（只有扣减数量时有用）
        /// </summary>
        private Dictionary<string, int> productLotStore;
        public ProductLotManager()
        {
            // this.context = context;
            this.productLots = new List<ProductLot>();
            this.productLotStore = new Dictionary<string, int>();
        }
        /*
        /// <summary>
        /// 采购单外的单据 扣减 产品批次库存
        /// </summary>
        /// <param name="city"></param>
        /// <param name="product"></param>
        /// <param name="num"></param>
        public void ReduceProductLot(StoreHouse storeHouse, Product product, int num)
        {
            int storeNum = 0;

            if (productLotStore.ContainsKey(product.Id))
            {
                storeNum = productLotStore[product.Id];
            }
            else
            {
                productLotStore.Add(product.Id, 0);
            }

            HandleProductLotByRecursion(storeHouse, product, storeNum, num);
        }

        /// <summary>
        /// 递归处理批次库存扣减
        /// </summary>
        /// <param name="city"></param>
        /// <param name="product"></param>
        /// <param name="num"></param>
        private void HandleProductLotByRecursion(StoreHouse storeHouse, Product product, int storeNum, int num)
        {
            ProductLot tempProductLot;
            while (storeNum > 0)
            {
                //当storeNum 大于0时，批次缓存List中肯定有Num 大于0的项，否则是逻辑错误
                tempProductLot = productLots.Where(l => l.Num > 0).OrderBy(l => l.ExpirationDate)
                    .First(l => l.Product.Id == product.Id && l.StoreHouse.Id == storeHouse.Id);

                if (tempProductLot.Num >= num)
                {
                    productLotStore[product.Id] = storeNum - num;
                    tempProductLot.Num -= num;
                    return;
                }

                //这里 缓存的批次库存小于扣除数量，需要继续加载批次
                num = num - tempProductLot.Num;
                productLotStore[product.Id] = storeNum = storeNum - tempProductLot.Num;
                tempProductLot.Num = 0;
            }
            //已查询过的批次
            string[] productLotIds = productLots.Where(l => l.Product.Id == product.Id && l.StoreHouse.Id == storeHouse.Id)
                .Select(l => l.Id).ToArray();

            tempProductLot = context.ProductLots.Include("Product").Where(l => l.Num > 0 && !productLotIds.Contains(l.Id)
                  && l.StoreHouse.Id == storeHouse.Id && l.Product.Id == product.Id)
                  .OrderBy(l => l.ExpirationDate).FirstOrDefault();
            //批次库存已经全部扣减完毕，不在继续
            if (tempProductLot == null)
                return;

            productLots.Add(tempProductLot);
            storeNum = tempProductLot.Num;
            if (storeNum >= num)
            {
                productLotStore[product.Id] = storeNum - num;
                tempProductLot.Num -= num;
                return;
            }

            //这里 缓存的批次库存小于扣除数量，需要继续加载批次
            num = num - storeNum;
            productLotStore[product.Id] = storeNum = tempProductLot.Num = 0;

            HandleProductLotByRecursion(storeHouse, product, storeNum, num);
        }


        /// <summary>
        /// 非采购单审核 增加批次库存
        /// </summary>
        /// <param name="city"></param>
        /// <param name="product"></param>
        /// <param name="num"></param>
        public void AddProductLot(StoreHouse storeHouse, Product product, int num)
        {
            var tempProductLog = productLots.FirstOrDefault(l => l.Product.Id == product.Id && l.StoreHouse.Id == storeHouse.Id);
            if (tempProductLog == null)
            {
                tempProductLog = context.ProductLots.Where(l => l.Num > 0).OrderBy(l => l.ExpirationDate)
                    .FirstOrDefault(l => l.Product.Id == product.Id && l.StoreHouse.Id == storeHouse.Id);

                if (tempProductLog == null)
                {
                    tempProductLog = context.ProductLots.OrderByDescending(l => l.ExpirationDate)
                        .FirstOrDefault(l => l.Product.Id == product.Id && l.StoreHouse.Id == storeHouse.Id);
                }
            }

            if (tempProductLog != null)
                tempProductLog.Num += num;
        }

        public void AddProductLot(BaseNote note)
        {
           if (!(note.ExpirationDate.HasValue && note.ProductionDate.HasValue))
           {
               AddProductLot(note.StoreHouse, note.Product, note.Num);
               return;
           }

           var tempProductLog = new ProductLot()
           {
               Product = note.Product,
               StoreHouse = note.StoreHouse,
               City = note.StoreHouse.City,
               InStockItem_Id = note.Id,
               PurchaseInStockNote_Id = note.NoteNo,
               ExpirationDate = note.ExpirationDate.Value,
               ProductionDate = note.ProductionDate.Value,
               Num = note.Num,
               Unit = note.Unit,
               PackageQuantity = note.Product.Info.Desc.PackageQuantity,
               Specifications = note.Product.Info.Desc.Specifications,

           };
           context.ProductLots.Add(tempProductLog);
           productLots.Add(tempProductLog);
        }

        public void ReduceProductLot(BaseNote note)
        {
           //无生产日期的 直接走老入口
           if (!(note.ExpirationDate.HasValue && note.ProductionDate.HasValue))
           {
               ReduceProductLot(note.StoreHouse, note.Product, note.Num);
               return;
           }

           int num = note.Num;
           int storeNum = 0;

           if (productLotStore.ContainsKey(note.Product.Id))
               storeNum = productLotStore[note.Product.Id];
           else
               productLotStore.Add(note.Product.Id, 0);

           ProductLot tempProductLot = productLots.FirstOrDefault(l => l.PurchaseInStockNote_Id == note.NoteNo
             && l.InStockItem_Id == note.Id && l.Product.Id == note.Product.Id);
           if (tempProductLot == null)
           {
               tempProductLot = context.ProductLots.Where(l => l.Num > 0).FirstOrDefault(l => l.PurchaseInStockNote_Id == note.NoteNo
               && l.InStockItem_Id == note.Id && l.Product.Id == note.Product.Id);
               if (tempProductLot != null)
               {
                   productLots.Add(tempProductLot);

                   if (tempProductLot.Num >= note.Num)
                   {
                       tempProductLot.Num -= num;
                       productLotStore[note.Product.Id] = storeNum = tempProductLot.Num + storeNum;
                       return;
                   }
                   else
                   {
                       num -= tempProductLot.Num;
                       tempProductLot.Num = 0;
                   }
               }
           }

           HandleProductLotByRecursion(note.StoreHouse, note.Product, storeNum, num);
        }


        /// <summary>
        /// 采购单审核增加产品批次
        /// </summary>
        /// <param name="note"></param>
        /// <param name="item"></param>
        public void AddProductLot(PurchaseInStockNote note, InStockItem item)
        {
            if (!(item.ExpirationDate.HasValue && item.ProductionDate.HasValue))
                return;

            var tempProductLog = new ProductLot()
                {
                    Product = item.Product,
                    StoreHouse = note.StoreHouse,
                    City = note.City,
                    InStockItem_Id = item.Id,
                    PurchaseInStockNote_Id = note.NoteNo,
                    Version = note.Version,
                    ExpirationDate = item.ExpirationDate.Value,
                    ProductionDate = item.ProductionDate.Value,
                    Num = item.Num,
                    Unit = item.Unit,
                    PackageQuantity = item.Product.Info.Desc.PackageQuantity,
                    Specifications = item.Product.Info.Desc.Specifications,
                    Provider = note.Supplier,
                };
            context.ProductLots.Add(tempProductLog);
            productLots.Add(tempProductLog);
        }

        /// <summary>
        /// 采购单反审核扣减产品批次
        /// 扣减流程：先扣减采购单审核时产生的批次库存，再扣减已加载的批次库存，最后扣减从数据库中新加载的批次库存
        /// </summary>
        /// <param name="note"></param>
        /// <param name="item"></param>
        public void ReduceProductLot(PurchaseInStockNote note, InStockItem item)
        {
            if (!(item.ExpirationDate.HasValue && item.ProductionDate.HasValue))
                return;

            int num = item.Num;
            int storeNum = 0;

            if (productLotStore.ContainsKey(item.Product.Id))
                storeNum = productLotStore[item.Product.Id];
            else
                productLotStore.Add(item.Product.Id, 0);

            ProductLot tempProductLot = productLots.FirstOrDefault(l => l.PurchaseInStockNote_Id == note.NoteNo
              && l.InStockItem_Id == item.Id && l.Product.Id == item.Product.Id);
            if (tempProductLot == null)
            {
                tempProductLot = context.ProductLots.Where(l => l.Num > 0).FirstOrDefault(l => l.PurchaseInStockNote_Id == note.NoteNo
                && l.InStockItem_Id == item.Id && l.Product.Id == item.Product.Id);
                if (tempProductLot != null)
                {
                    productLots.Add(tempProductLot);

                    if (tempProductLot.Num >= item.Num)
                    {
                        tempProductLot.Num -= num;
                        productLotStore[item.Product.Id] = storeNum = tempProductLot.Num + storeNum;
                        return;
                    }
                    else
                    {
                        num -= tempProductLot.Num;
                        tempProductLot.Num = 0;
                    }
                }
            }

            HandleProductLotByRecursion(note.StoreHouse, item.Product, storeNum, num);
        }*/

    }
}
