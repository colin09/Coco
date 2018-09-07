

var vmConfig = avalon.define({
    $id: "vm-position",
    list: [],
    config: {
        nName: "new name",
    },
    item: {
        code:"",
        nName: "new name",
        nEnName: "",
        nAlias: "",
        nImage: "",
        nDesc: "",
        nType:1
    },
    showImg: function (item) {

        $("#newId").val(item.id);
    },
    del: function (item) {
        $("#bodymodal").modal("show");
        $("#bodymodal .modal-body").html("<p>确定要删除吗？</p>");
        $("#bodymodal .btnSubmit").data("id", item.id).data("opt", "del-store");
        console.log(this);
        //Delete(item.id);
    },
    create: function () {
        $("#newId").val(0);
        Modify(true);
    },
    reset: function () {
        vmConfig.item.nName = "";
        vmConfig.item.nEnName = "";
        vmConfig.item.nAlias = "";
        vmConfig.item.nImage = "";
        vmConfig.item.nDesc = "";
    }
});

function Modify(isNew) {
    $.getJSON("/System/ModifyItem",
   {
       id: $("#newId").val(),
       code: $("#configCode").val(),
       name: vmConfig.item.nName,
       enName: vmConfig.item.nEnName,
       alias: vmConfig.item.nAlias,
       image: vmConfig.item.nImage,
       desc: vmConfig.item.nDesc,
       type: vmConfig.item.nType,
       isNew: isNew
   })
   .done(function (json) {
       if (json.message === true)
           ReadItems(vmConfig.item.code);
       else
           alert(json.message);
   })
   .fail(function (error) {
       console.log("Request Failed: " + error);
   });
}
