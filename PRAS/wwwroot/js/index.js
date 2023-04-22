jQuery(document).ready(function () {

	$('#logout').on('click', function () {
		$.ajax({
			url: 'https://localhost:7285/authentication/logout',
			type: 'POST',
			success: function (response) {
				window.location.href = "https://localhost:7285/news/index"
			}
		});
	});

});