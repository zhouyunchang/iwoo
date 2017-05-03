
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
    });

})(window, document, window.jQuery);


$(document).ready(function () {


    $('#addsubmit').click(function () {
        $.ajax({
            type: 'post',
            url: '/StaffManagement/AddUserInfo',
            data: $("#addform").serialize(),
            dataType: 'json',
            success: function (data) {
                if (data.flag) {
                    alert(data.msg);
                } else {
                    alert(data.msg);
                }
            }
        });
    });

    $('#updatesubmit').click(function () {
        $.ajax({
            type: 'post',
            url: '/StaffManagement/UpdateUserInfo',
            data: $("#updateform").serialize(),
            dataType: 'json',
            success: function (data) {
                if (data.flag) {
                    alert(data.msg);
                } else {
                    alert(data.msg);
                }
            }
        });
    });

    $(document).on('click', '.delete', function () {
        if (confirm('是否删除？')) {
            $.ajax({
                type: 'post',
                url: '/StaffManagement/DelUserInfo',
                data: { id: $(this).parent().parent().find('td:eq(0)').text() },
                dataType: 'json',
                success: function (data) {
                    if (data) {
                        window.location.reload();
                        alert("删除成功");

                    } else {
                        alert("删除失败");
                    }
                }
            });
        }
    });




});
