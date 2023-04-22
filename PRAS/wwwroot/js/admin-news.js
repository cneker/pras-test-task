jQuery(document).ready(function () {

	$('#file').on('change', function () {
		let file = $('#file')[0].files[0];
		var reader = new FileReader();
		reader.onload = function () {
			document.getElementById("image").src = reader.result;
			document.getElementById("image").name = file.name;
		}
		reader.readAsDataURL(file);
	});

	$('#delete').on('click', function () {
		let id = $('#id').val();
		$.ajax({
			url: `https://localhost:7285/admin/news?newsId=${id}`,
			type: 'DELETE',
			success: function (response) {
				window.location.href = "https://localhost:7285/admin"
			}
		});
	});

	$('#update').on('click', function () {
		let id = $('#id').val();

		let title = $('#title').val();
		let subtitle = $('#subtitle').val();
		let description = $('#description').val();
		let img = $('#image')[0];
		let base64 = img.src.replace(/^data:.+;base64,/, '');

		$.ajax({
			url: `https://localhost:7285/admin/news?newsId=${id}`,
			type: 'PUT',
			headers: {
				'Content-Type': 'application/json'
			},
			data: JSON.stringify({
				Title: title,
				Subtitle: subtitle,
				Description: description,
				Base64String: base64,
				FileName: img.name
			}),
			dataType: 'json',
			success: function (response) {
				window.location.href = "https://localhost:7285/admin"
			}
		});
	});
});