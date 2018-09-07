var vm = avalon.define({
    $id: "test",
    name: "司徒正美",
    phone: "",
    d: [],
    string: "xxx",
    bool: true,
    number: 100,
    object: {
        aaa: "aaa",
        bbb: "bbb"
    },
    add: function () { }
});



var vmStore = avalon.define({
    $id: "vm-store",
    list: [],
    pageCount: 1,
    nName: "new name",
    nAddr: "",
    nContact: "",
    nMobile: "",
    nEmail: "",
    qrcode: function (item) {
        $("#bodymodal .modal-body").html("<img src='" + item.qrCode + "' />");
        $("#bodymodal .btnSubmit").data("id", item.id).data("opt", "qrcode");
        $("#bodymodal").modal("show");
        console.log(this);
    },
    show: function (item) {
        vmStore.nName = item.name;
        vmStore.nAddr = item.address;
        vmStore.nContact = item.contract;
        vmStore.nMobile = item.mobile;
        vmStore.nEmail = item.email;
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
        Create();
    },
    reset: function () {
        vmStore.nName = "";
        vmStore.nAddr = "";
        vmStore.nContact = "";
        vmStore.nMobile = "";
        vmStore.nEmail = "";
    }
});

vmStore.$watch("mobile",
    function (a) {
        var b = a.replace(/\s+/g, "");
        var array = b.split("");
        var ret = "";
        for (var i = 0, n = array.length; i < n; i++) {
            if (i > 10) //不能超过11位
                break;
            if (i === 3) {
                ret += " ";
            }
            if (i === 7) {
                ret += " ";
            }
            ret += array[i];
        }
        vm.phone = ret;
    });

$(function () {

    ReadList(1, 12);
    $("#bodymodal .btnSubmit").click(function () {
        var id = $(this).data("id");
        var opt = $(this).data("opt");
        switch (opt) {
            case "del-store":
                Delete(id);
                break;
            default:
                break;
        }

        $("#bodymodal").modal("hide");
    });
    $("#bodymodal .btnCancel").click(function () {

    });
   


});

function pageClick(el) {
    var num = $(el).data("page");
    var pageIndex = $(".pagination").data("index");
    var pageCount = $(".pagination li").filter(".num").length;
    var pageSize = 12;

    console.log(num);
    switch (num) {
        case "-1":
            if (pageIndex > 1)
                ReadList(--pageIndex, pageSize);
            break;
        case "+1":
            if (pageIndex < pageCount)
                ReadList(++pageIndex, pageSize);
            break;
        default:
            ReadList(num, pageSize);
            break;
    };
}

function ReadList(page, pageSize) {
    $.getJSON("/store/list", { page: page, pageSize: pageSize })
    .done(function (json) {
        if (json.total > 0) {
            vmStore.list = json.item;
            vmStore.pageCount = json.pageCount;
            var pagelayout = "";
            for (var i = 0; i < json.pageCount; i++) {
                pagelayout += "<li data-page='" + (i + 1) + "' class='num'><a href='#'>" + (i + 1) + "</a></li>";
            }
            $(".pagination li").filter(".num").remove();
            $(pagelayout).insertAfter(".pagination li:first");

            $(".pagination li").click(function () {
                pageClick($(this));
            });
        }
    })
    .fail(function (error) {
        console.log("Request Failed: " + error);
    });
}


function Create() {
    $.getJSON("/store/modify",
    {
        id: $("#newId").val(),
        name: vmStore.nName,
        addr: vmStore.nAddr,
        contact: vmStore.nContact,
        mobile: vmStore.nMobile,
        email: vmStore.nEmail
    })
    .done(function (json) {
        if (json.message === true)
            ReadList(1, 12);
        else
            alert(json.message);
    })
    .fail(function (error) {
        console.log("Request Failed: " + error);
    });
}


function Delete(id) {
    $.getJSON("/store/delete", { id: id })
    .done(function (json) {
        ReadList(1, 12);
    })
    .fail(function (error) {
        console.log("Request Failed: " + error);
    });
}