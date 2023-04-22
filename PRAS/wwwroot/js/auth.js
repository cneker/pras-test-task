jQuery(document).ready(function () {

	$('#login').on('click', function () {
		let email = $('#email').val();
		let password = $('#password').val();
		$.ajax({
			url: 'https://localhost:7285/authentication/login',
			type: 'POST',
			headers: {
				'Content-Type': 'application/json'
			},
			data: JSON.stringify({ Email: email, Password: password }),
			success: function (response) {
				window.location.href = "https://localhost:7285/admin"
			}
		});
	});

});