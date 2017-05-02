// Demo datatables
// ----------------------------------- 


(function (window, document, $, undefined) {

    $(function () {

        //
        // Zero configuration
        // 

        $('#datatable1').dataTable({
            'paging': true,  // Table pagination
            'ordering': true,  // Column ordering 
            'info': true,  // Bottom left status text
            // Text translation options
            // Note the required keywords between underscores (e.g _MENU_)
            oLanguage: {
                sSearch: 'Search all columns:',
                sLengthMenu: '_MENU_ records per page',
                info: 'Showing page _PAGE_ of _PAGES_',
                zeroRecords: 'Nothing found - sorry',
                infoEmpty: 'No records available',
                infoFiltered: '(filtered from _MAX_ total records)'
            }
        });


        // 
        // Filtering by Columns
        // 

        var dtInstance2 = $('#datatable2').dataTable({
            'paging': true,  // Table pagination
            'ordering': true,  // Column ordering 
            'info': true,  // Bottom left status text
            // Text translation options
            // Note the required keywords between underscores (e.g _MENU_)
            oLanguage: {
                sSearch: 'Search all columns:',
                sLengthMenu: '_MENU_ records per page',
                info: 'Showing page _PAGE_ of _PAGES_',
                zeroRecords: 'Nothing found - sorry',
                infoEmpty: 'No records available',
                infoFiltered: '(filtered from _MAX_ total records)'
            }
        });
        var inputSearchClass = 'datatable_input_col_search';
        var columnInputs = $('tfoot .' + inputSearchClass);

        // On input keyup trigger filtering
        columnInputs
            .keyup(function () {
                dtInstance2.fnFilter(this.value, columnInputs.index(this));
            });


        // 
        // Column Visibilty Extension
        // 

        $('#datatable3').dataTable({
            'paging': true,  // Table pagination
            'ordering': true,  // Column ordering 
            'info': true,  // Bottom left status text
            // Text translation options
            // Note the required keywords between underscores (e.g _MENU_)
            oLanguage: {
                sSearch: 'Search all columns:',
                sLengthMenu: '_MENU_ records per page',
                info: 'Showing page _PAGE_ of _PAGES_',
                zeroRecords: 'Nothing found - sorry',
                infoEmpty: 'No records available',
                infoFiltered: '(filtered from _MAX_ total records)'
            },
            // set columns options
            'aoColumns': [
                { 'bVisible': false },
                { 'bVisible': true },
                { 'bVisible': true },
                { 'bVisible': true },
                { 'bVisible': true }
            ],
            sDom: 'C<"clear">lfrtip',
            colVis: {
                order: 'alfa',
                'buttonText': 'Show/Hide Columns'
            }
        });

        // 
        // AJAX
        // 

        $('#datatable4').dataTable({
            "oLanguage": {
                "sLengthMenu": "每页显示 _MENU_ 条记录",
                "sZeroRecords": "抱歉， 没有找到",
                "sInfo": "从 _START_ 到 _END_ /共 _TOTAL_ 条数据",
                "sInfoEmpty": "没有数据",
                "sInfoFiltered": "(从 _MAX_ 条数据中检索)",
                "oPaginate": {
                    "sFirst": "首页",
                    "sPrevious": "前一页",
                    "sNext": "后一页",
                    "sLast": "尾页"
                },
                "sZeroRecords": "没有检索到数据",
                "sSearch": "搜索",
                "sProcessing": "<img src='./loading.gif' />"
            },
            'paging': true,  // Table pagination
            'ordering': true,  // Column ordering 
            'info': true,  // Bottom left status text
            sAjaxSource: '/Content/Json/datatable.json',
            aoColumns: [
                { mData: 'engine' },
                { mData: 'browser' },
                { mData: 'platform' },
                { mData: 'version' }
            ],
            "columnDefs": [
                {
                    "targets":4,
                    "data": "grade",
                    "render": function (data, type, row) {
                        return '<a href="#" data-toggle="modal" data-target="#UpdateModal">修改 </a> | <a href="' + data +'"> 删除</a> ';
                    },

                }
            ]
        });
    });

})(window, document, window.jQuery);
