jQuery(document).ready(function () {

	var page = 1;
	var _inCallback = false;
	function loadItems() {
		if (page > -1 && !_inCallback) {
			_inCallback = true;
			page++;

			$.ajax({
				type: 'GET',
				url: 'https://localhost:7285/admin?PageNumber=' + page,
				success: function (data, text, xhr) {
					let temp = $.parseJSON(xhr.getResponseHeader("X-Pagination"))

					$("#scrolList").append(data);
					if (temp['HasNext'] == false) {
						page = -1;
						$('#load-more')[0].style.visibility = "hidden";
					}
					_inCallback = false;
				}
			});
		}
	}

	$('#load-more').on('click', async function () {
		loadItems();
	});

	$('#file').on('change', function () {
		let file = $('#file')[0].files[0];
		var reader = new FileReader();
		reader.onload = function () {
			document.getElementById("image").src = reader.result;
		}
		reader.readAsDataURL(file);
	});

	$('#create').on('click', async function () {

		let title = $('#title').val();
		let subtitle = $('#subtitle').val();
		let description = $('#description').val();
		let file = $('#file')[0].files[0];
		let fileName = '';
		let base64 = '';
		if (typeof file !== 'undefined') {
			const toBase64 = file => new Promise((resolve, reject) => {
				const reader = new FileReader();
				reader.readAsDataURL(file);
				reader.onload = () => resolve(reader.result.replace(/^data:.+;base64,/, ''));
				reader.onerror = error => reject(error);
			});

			let promise = toBase64(file);
			base64 = await promise;
			fileName = file.name;
		}

		$.ajax({
			url: 'https://localhost:7285/admin',
			type: 'POST',
			headers: {
				'Content-Type': 'application/json'
			},
			data: JSON.stringify({
				Title: title,
				Subtitle: subtitle,
				Description: description,
				Base64String: base64,
				FileName: fileName
			}),
			complete: function (response) {
				window.location.href = "https://localhost:7285/admin"
			}
		});
	});

	

});