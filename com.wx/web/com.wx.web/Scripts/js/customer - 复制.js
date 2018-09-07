

var vmCustomer = avalon.define({
    $id: "vm-customer",
    code: 0,
    title: "",
    description:"",

    dressCode: 10,
    photoCode: 11,
    
    pageIndex: 1,
    pageMain: 2,
    pageDetail: 3,
    
    list: [], 
});



$(function () {
    ReadList(vmCustomer.dressCode, vmCustomer.pageMain);
});

function ReadList(code,page) {
    $.getJSON("ItemData", { code: $("#hdCode").val(), type: $("#hdType").val(), sourceId: $("#hdSource").val() })
   .done(function (json) {
       if (json.length > 0) {
           vmCustomer.list = json;

           vmCustomer.title = json[0].Name;
           vmCustomer.description = json[0].Desc;
       }
       //$('#list li').wookmark();
   })
   .fail(function (error) {
       console.log("Request Failed: " + error);
   });
}
