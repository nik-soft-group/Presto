

function callApi(request) {
    $.post(request.url, request.data, function (data, status, xhr) {
        if (status === 'success') {
            request.success(data);
        } else {
            console.log('error: ' + xhr);
        }
    });
}

function dropdownBinder(sets) {
    $(sets.ddl).html('');
    $(sets.ddl).append($('<option></option>').val(0).html('انتخاب کنید'));
    sets.data.forEach(function (item, index) {
        $(sets.ddl).append($('<option></option>').val(item.id).html(item.title));
    });
}

function setItemDropdown(sets) {
    $(sets.ddl).val(sets.value);
}

function showMessage(sets) {
    new Noty({
        layout: 'topLeft',
        text: sets.text,
        type: sets.type,
        theme: 'sunset',
        progressBar: true,
        timeout: 4000
    }).show();
}

$(document).ready(function(){
	tinymce.init({
        selector: '.text-editor',
		directionality : 'ltr',
		height : "500"
    });
});