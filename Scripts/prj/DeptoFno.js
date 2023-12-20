$(document).ready(function () {
    //Master編輯容器加入Detail
    douoptions.afterCreateEditDataForm = function ($container, row) {
        DeptoFno($container, "CkNo1", "CkNo1_Dep", row);
        DeptoFno($container, "CkNo2", "CkNo2_Dep", row);
        DeptoFno($container, "CkNo3", "CkNo3_Dep", row);
        DeptoFno($container, "CkNo4", "CkNo4_Dep", row);
        DeptoFno($container, "CkNo5", "CkNo5_Dep", row);
        DeptoFno($container, "AgentNo", "AgentNo_Dep", row);
        DeptoFno($container, "kpino1", "kpino1_Dep", row);
        DeptoFno($container, "kpino2", "kpino2_Dep", row);
        DeptoFno($container, "kpino3", "kpino3_Dep", row);
        DeptoFno($container, "kpino4", "kpino4_Dep", row);
        DeptoFno($container, "kpino5", "kpino5_Dep", row);

        DeptoFno($container, "DCkNo1", "DCkNo1_Dep", row);
        DeptoFno($container, "DCkNo2", "DCkNo2_Dep", row);
        DeptoFno($container, "DCkNo3", "DCkNo3_Dep", row);
        DeptoFno($container, "DAdmino", "DAdmino_Dep", row);
        DeptoFno($container, "DCkTopNo", "DCkTopNo_Dep", row);
    }
    var DeptoFno = function ($container, ef, df, row) {
        var $_depselect = $container.find('select[data-fn="' + df + '"]');
        var $_fnoselect = $container.find('select[data-fn="' + ef + '"]');
        var $_fnoptions = $_fnoselect.find('option');
        $_depselect.prop('disabled', false);
        $_fnoselect.closest('.form-group').insertAfter($_depselect.closest('.form-group')) //將員工select移置部門後
        $_depselect.on('change', function () {
            console.log('C=' + $_depselect.val());
            var dv = $_depselect.val();
            $_fnoselect.empty();
            if (dv == '')
                $_fnoptions.appendTo($_fnoselect);
            //$_fnodelect.find('option').removeClass('d-none');
            else {
                //$_fnoselect.find('option').addClass('d-none').first().removeClass('d-none'); //不隱藏第一個所有選項
                if ($_fnoptions.first().val() == '')
                    $_fnoptions.first().appendTo($_fnoselect);
                $.each($_fnoptions, function () {
                    var $_this = $(this);
                    if ($_this.attr("data-dcode") == dv)
                        $_this.appendTo($_fnoselect);
                });
                //$_fnoptions.find('[data-dcode="' + dv + '"]').appendTo($_fnoselect);//.removeClass('d-none');
            }
            //if (changeslectfist && $_fnoselect)
            //    ''
            //else
            //    $_fnoselect.val('');
            if ($_fnoselect.find('option').length > 0)
                $_fnoselect.selectIndex = 0;
        })
    }
    $("#_table").DouEditableTable(douoptions); //初始dou table
});