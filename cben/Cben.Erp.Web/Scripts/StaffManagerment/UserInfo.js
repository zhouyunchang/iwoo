
(function (window, document, $, undefined) {

    $(function () {
        $('#tbEmployee').dataTable({
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
                url: '/StaffManagement/DelUserInfo',
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
                url: '/StaffManagement/GetUserInfo',
                data: { id: id },
                dataType: 'json',
                success: function (data) {

                    modal.find('#updateModal_txtUserId').val(data.Id);
                    modal.find('#updateModal_userCode').val(data.SerialNumber);
                    modal.find('#updateModal_userName').val(data.User.UserName);
                    modal.find('#updateModal_realName').val(data.User.Name);
                    modal.find('#updateModal_phoneNum').val(data.User.PhoneNumber);

                }
            });

        });

    });

})(window, document, window.jQuery);


$(document).ready(function () {


    $('#addsubmit').click(function () {
        $.ajax({
            type: 'post',
            url: '/StaffManagement/AddUserInfo',
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

    $('#updatesubmit').click(function () {
        $.ajax({
            type: 'post',
            url: '/StaffManagement/UpdateUserInfo',
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
