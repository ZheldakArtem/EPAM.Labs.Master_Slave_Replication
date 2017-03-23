$(document).ready(function () {

	$("#showChart").click(function () {
		var canvas = document.getElementById("myChart");
		var ctx = canvas.getContext('2d');

		var myChart = new Chart(ctx,
		{
			type: 'line',
			data: {
				labels: window.numbReq,
				datasets: [
					{
						label: 'master',
						data: window.masterData,
						backgroundColor: "rgba(153,255,51,0.6)"
					},
					{
						label: 'slaves',
						data: window.slavesData,
						backgroundColor: "rgba(255,153,0,0.6)"
					}
				]
			}
		});
	});
});


