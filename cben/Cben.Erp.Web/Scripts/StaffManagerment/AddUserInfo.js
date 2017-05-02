//function afterAdd(data) {
//    alert('asdf');
//    alert(data.msg);
//};


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


$(document).ready(function () {


    //function afterAdd(data) {
    //    alert('asdf');
    //    alert(data.msg);
    //}

    //$('a').click(function () {
    //    alert("asdf");
    //});
});
