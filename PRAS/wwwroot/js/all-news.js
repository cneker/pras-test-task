jQuery(document).ready(function () {

	var page = 1;
	var _inCallback = false;
	function loadItems() {
		if (page > -1 && !_inCallback) {
			_inCallback = true;
			page++;
			$.ajax({
				type: 'GET',
				url: 'https://localhost:7285/News/allnews?PageNumber=' + page,
				success: function (data, text, xhr) {
					let temp = JSON.parse(xhr.getResponseHeader("X-Pagination"))

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

});