$(function () {
    $("#SearchBox").change(function () {
        var country = $(this).find(":selected").val();
        google.charts.load('current', { 'packages': ['corechart'] });
        google.charts.setOnLoadCallback(drawCountryChart);
        function drawCountryChart() {
            $.ajax({
                url: '/Index?handler=Search',
                data: { 'id': country },
                dataType: 'json',
                contentType: 'application/json',
                type: 'GET',
                success: MyCountryChart
            });
            function MyCountryChart(data) {
                var drawData = new google.visualization.DataTable();
                drawData.addColumn('string', 'Country');
                drawData.addColumn('number', 'NewConfirmed');
                drawData.addColumn('number', 'TotalConfirmed');
                drawData.addColumn('number', 'NewDeaths');
                drawData.addColumn('number', 'TotalDeaths');
                drawData.addColumn('number', 'NewRecovered');
                drawData.addColumn('number', 'TotalRecovered');
                for (var i = 0; i < data.length; i++) {
                    drawData.addRow
                        ([
                            data[i].Country,
                            data[i].NewConfirmed,
                            data[i].TotalConfirmed,
                            data[i].NewDeaths,
                            data[i].TotalDeaths,
                            data[i].NewRecovered,
                            data[i].TotalRecovered
                        ]);
                };
                var options = {
                    'title': 'Country Covid Data'
                };
                var chart = new google.visualization.BarChart(document.getElementById('ChartForCountry'));
                chart.draw(drawData, options);
            };
        }
    });
});