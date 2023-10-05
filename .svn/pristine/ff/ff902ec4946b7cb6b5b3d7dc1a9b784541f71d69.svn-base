//Required Validators

function obj_required(obj)
{
    $('#' + obj).on('input', function () {
        var input = $(this);
        var is_name = input.val();
        if (is_name) { input.removeClass("invalid").addClass("valid"); }
        else { input.removeClass("valid").addClass("invalid"); }
    });
}