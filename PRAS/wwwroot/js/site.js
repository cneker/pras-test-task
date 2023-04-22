$.ajaxSetup({
	error: function (jqXHR, exception) {
		var err = JSON.parse(jqXHR.responseText);
		if (jqXHR.status == 500) {
			alert('Internal Server Error (500).');
		} else {
			alert(err.message);
		}
	}
});