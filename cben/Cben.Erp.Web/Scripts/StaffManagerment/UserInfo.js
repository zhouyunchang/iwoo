

$(document).on('click', '#addsubmit', function () {
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

$(document).on('click', '#updatesubmit', function () {
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


$(document).ready(function () {


    //function afterAdd(data) {
    //    alert('asdf');
    //    alert(data.msg);
    //}

    //$('a').click(function () {
    //    alert("asdf");
    //});
});
