$(document).ready(function () {
    //var d = new Date();
    //var fn = '資產總表' + formatDate(d, 4);
    //douoptions.tableOptions.buttonsAlign = 'none';
    //douoptions.tableOptions.buttonsClass = 'btn btn-secondary';
    //douoptions.tableOptions.showExport = true;
    //douoptions.tableOptions.iconsPrefix = "glyphicon";
    //douoptions.tableOptions.icons = { export: 'glyphicon-export' };
    //douoptions.tableOptions.exportTypes = ['csv'];
    //douoptions.tableOptions.exportOptions = { fileName: fn};
    //douoptions.tableOptions.exportDataType = 'all';
    //douoptions.tableOptions.formatExport = function () { return "匯出畫面顯示資產表" };
    douoptions.tableOptions.pageList = [10, 25, 50, 100, 'All'];

    var $_editDataContainer = undefined;
    var hasChangeDetails = false;//是否已變更Details資料

    douoptions.afterCreateEditDataForm = function ($container, row) {

        hasChangeDetails = false;
        if (row.AssetID == undefined)
            return;
        $.getJSON($.AppConfigOptions.baseurl + 'UsageLog/GetDataManagerOptionsJson', function (_opt) {
            //取消自動抓後端資料
            _opt.tableOptions.url = undefined;
            //20230503, edit 保管移轉紀錄編輯功能打開 by markhong
            _opt.addable = _opt.deleteable = false;
            //_opt.addable = _opt.editable = _opt.deleteable = false; 

            //給detail集合
            row.UsageLog = row.UsageLog || []; //無detail要實體參考，之後detail編輯才能跟master有關聯(前端物件)
            _opt.datas = row.UsageLog;

            //初始options預設值
            douHelper.setFieldsDefaultAttribute(_opt.fields);//給預設屬性

            //編輯後給detail.MasterId
            _opt.afterEditDataConfirm = function (drow, callback) {
                drow.AssetsAssetID = row.AssetID; //[重要]AssetsAssetID = [dbo].[Assets]+[dbo].[Assets].[AssetID]
                callback();
            }

            //Master的編輯物件
            var $_oform = $container.find(".data-edit-form-group");

            //Detail的編輯物件
            $_editDataContainer = $('<div style="background-color: #FFFFf1;padding: .5rem;border-radius: .5rem;">').appendTo($_oform.parent());
            //實體Dou js
            var $_detailTable = $('<table>').appendTo($_editDataContainer).douTable(_opt).
                on([$.dou.events.add, $.dou.events.update, $.dou.events.delete].join(' '), function () {
                    hasChangeDetails = true;
                });
        });
    }

    //還原已變更Details資料
    douoptions.afterEditDataCancel = function (r) {
        if (hasChangeDetails)
            douoptions.updateServerData(r, function (result) {
                $_masterTable.DouEditableTable('updateDatas', result.data);//取消編輯，detail有可能已做一些改變，故重刷UI
            })
    }
    //新增Col：資訊設備
    douoptions.fields.push({
        title: "IT明細", field: "ITButton", align: "center", formatter: detailbtn, visibleEdit: false
    });

    douoptions.fields.push({ title: "轉移", field: "UsageLog", formatter: function (v) { console.log(v); if (v != null) return v.length }, visibleEdit: false });
    var $_masterTable = $("#_table").DouEditableTable(douoptions);

    /*douoptions.tableOptions.bootstrapTable = { Pagination: true, pageSize: 15, };*/
});

function formatDate(dateObj, format) {
    var monthNames = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
    var curr_date = dateObj.getDate();
    var curr_month = dateObj.getMonth();
    curr_month = curr_month + 1;
    var curr_year = dateObj.getFullYear();
    var curr_min = dateObj.getMinutes();
    var curr_hr = dateObj.getHours();
    var curr_sc = dateObj.getSeconds();
    if (curr_month.toString().length == 1)
        curr_month = '0' + curr_month;
    if (curr_date.toString().length == 1)
        curr_date = '0' + curr_date;
    if (curr_hr.toString().length == 1)
        curr_hr = '0' + curr_hr;
    if (curr_min.toString().length == 1)
        curr_min = '0' + curr_min;
    if (curr_sc.toString().length == 1)
        curr_sc = '0' + curr_sc;

    if (format == 1)//dd-mm-yyyy
    {
        return curr_date + "-" + curr_month + "-" + curr_year;
    }
    else if (format == 2)//yyyy-mm-dd
    {
        return curr_year + "-" + curr_month + "-" + curr_date;
    }
    else if (format == 3)//dd/mm/yyyy
    {
        return curr_date + "/" + curr_month + "/" + curr_year;
    }
    else if (format == 4)// yyyyMMddHHmmss
    {
        return curr_year + curr_month + curr_date + curr_hr + curr_min + curr_sc;
    }
}

//資產盤點報表輸出
function AssetInventoryExport() {
    $.ajax({
        type: "POST",
        url: '../Asset/InventoryRpt',
        success: function (res) {
            alert("檔案下載完成");
            console.log('success');
            $("#textBox1").val(res);
        },
        error: function (request) {
            alert("Error");
        }
    });
}

//畫面資產表輸出
function exportData() {
    var d = new Date();
    var fn = '資產表' + formatDate(d, 4);
    $('#_table').tableExport({
        type: 'csv',
        exportDataType: "all",
        ignoreColumn: [19, 20],//忽略某一列的索引
        fileName: fn,//下载文件名称
        onCellHtmlData: function (cell, row, col, data) {//处理导出内容,自定义某一行、某一列、某个单元格的内容
            console.info(data);
            return data;
        },
    });
}

//展開<超連結>
function detailbtn(cellvalue, options, rowObject) {
    var vSN, vOS, vOffice, vCPU, vRam, vSSD, vHDD;
    var vIsIT = options.IsIT;
    vSN = vOS = vOffice = vCPU = vRam = vRam = vSSD = vHDD = '';
    if (options.ITAttr != "undefined" && options.ITAttr != null) {
        if (options.ITAttr[0] != "undefined" && options.ITAttr[0] != null) {
            for (let [key, value] of Object.entries(options.ITAttr[0])) {
                switch (key) {
                    case 'SN':
                        vSN = value;
                        break;
                    case 'OS':
                        vOS = value;
                        break;
                    case 'OfficeVersion':
                        vOffice = value;
                        break;
                    case 'Iserise':
                        vCPU = value;
                        break;
                    case 'RAM':
                        vRam = value;
                        break;
                    case 'SSD':
                        vSSD = value;
                        break;
                    case 'HDD':
                        vHDD = value;
                        break;
                    default:
                        break;
                }
            }
        }
    }
    var vcell = "'" + options.AssetID + "|" + vSN + "|" + vOS + "|" + vOffice + "|" + vCPU + "|" + vRam + "|" + vSSD + "|" + vHDD + "'";
    console.log(vcell);
    if (vIsIT)
        return '<a href="javascript:ShowRPT(' + vcell + ');"><i class="bi bi-list">展開</i></a>';
}

//細項popup
function ShowRPT(cellvalue) {
    const myArr = cellvalue.split("|");
    $("#AssetsAssetID").val(myArr[0]);
    $("#SN").val(myArr[1]);
    $("#OS").val(myArr[2]);
    $("#OfficeVersion").val(myArr[3]);
    $("#Iserise").val(myArr[4]);
    $("#RAM").val(myArr[5]);
    $("#SSD").val(myArr[6]);
    $("#HDD").val(myArr[7]);
    $("#ITPopUp").modal('show');
}

//細項取消
function closeDialog() {
    console.log("closed");
    $("#ITPopUp").modal('hide');
}

//細項編輯
function editInfo() {
    console.log("edit");
    var fd = new FormData();
    fd.append('AssetsAssetID', $("#AssetsAssetID").val());
    fd.append('SN', $("#SN").val());
    fd.append('OS', $("#OS").val().trim());
    fd.append('OfficeVersion', $("#OfficeVersion").val());
    fd.append('Iserise', $("#Iserise").val());
    fd.append('RAM', $("#RAM").val());
    fd.append('SSD', $("#SSD").val());
    fd.append('HDD', $("#HDD").val());
    console.log(fd);
    let confirmAction = confirm("確定要儲存嗎？");
    if (confirmAction) {
        $.ajax({
            dataType: 'json',
            url: '../ITAttributes/UpdateObject',
            //url: 'https://pj3.ftis.org.tw/AssetSys/ITAttributes/UpdateObject',
            type: "post",
            //data: new FormData($('#adj')['0']),
            data: fd,
            processData: false,
            contentType: false,
            success: function (data) {
                alert("儲存成功");
                location.reload();
            },

            error: function (request) {
                //alert(request.responseJSON.Message);
                alert("error");
            }
        });
    }
    else {
        alert("已取消儲存");
    }
    $("#ITPopUp").modal('hide');
}
