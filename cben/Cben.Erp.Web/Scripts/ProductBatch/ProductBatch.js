
(function (window, document, $, undefined) {

    $(function () {
        $('#tbProductBatch').dataTable({
            'paging': true,
            'ordering': true,
            'info': true,
            oLanguage: {
                sSearch: 'Search all columns:',
                sLengthMenu: '_MENU_ records per page',
                info: 'Showing page _PAGE_ of _PAGES_',
                zeroRecords: 'Nothing found - sorry',
                infoEmpty: 'No records available',
                infoFiltered: '(filtered from _MAX_ total records)'
            }
        });



        $('.btn-delete').click(function () {
            var id = $(this).attr('data-id');
            if (!confirm('是否删除？')) return;

            $.ajax({
                type: 'post',
                url: '/ProductBatch/DelProductBatch',
                data: { id: id },
                dataType: 'json',
                success: function (data) {
                    if (data) {
                        $.notify({ message: "删除成功", status: 'success' });
                        window.location.reload();
                    } else {
                        $.notify({ message: "删除失败", status: 'danger' });
                    }
                }
            });
        });


        $('#updateModal').on('show.bs.modal', function (event) {
            var button = $(event.relatedTarget);
            var id = button.attr('data-id');
            var modal = $(this);

            $.ajax({
                type: 'post',
                url: '/ProductBatch/GetProductBatchInfo',
                data: { id: id },
                dataType: 'json',
                success: function (data) {
                    modal.find('#updateModal_Id').val(data.Id);
                    modal.find('#updateModal_batchNo').val(data.BatchNo);
                    modal.find('#updateModal_spec').val(data.Spec);
                    modal.find('#updateModal_techNo').val(data.TechNo);
                    modal.find('#updateModal_diameter').val(data.Diameter);
                    modal.find('#updateModal_pressure').val(data.Pressure);
                }
            });

        });


        $('#addModal_submit').click(function () {
            $.ajax({
                type: 'post',
                url: '/ProductBatch/AddProductBatchInfo',
                data: $("#addForm").serialize(),
                dataType: 'json',
                success: function (data) {

                    if (data.flag) $('#addModal').modal('hide');

                    $.notify({
                        message: data.msg,
                        status: data.flag ? 'success' : 'danger'
                    })

                    window.location.reload();

                }
            });
        });


        $('#updateModal_submit').click(function () {
            $.ajax({
                type: 'post',
                url: '/ProductBatch/UpdateProductBatch',
                data: $("#updateForm").serialize(),
                dataType: 'json',
                success: function (data) {

                    if (data.flag) $('#updateModal').modal('hide');

                    $.notify({
                        message: data.msg,
                        status: data.flag ? 'success' : 'danger'
                    })

                    window.location.reload();
                }
            });
        });


    });

})(window, document, window.jQuery);

