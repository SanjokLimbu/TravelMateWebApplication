google.charts.load('current', { 'packages': ['corechart'] });
google.charts.setOnLoadCallback(drawChart);
function drawChart() {
    $.ajax({
        url: '/Index?handler=GlobalChartData',
        dataType: 'json',
        contentType: 'application/json',
        type: 'GET',
        success: MyChart
    });
    function MyChart(globalData) {
        var data = new google.visualization.DataTable();
        data.addColumn('string', 'GlobalCases');
        data.addColumn('number', 'Data');
        for (var i = 0; i < globalData.length; i++)
        {
            data.addRows([
                ['NewConfirmed', globalData[i].NewConfirmed],
                ['NewDeaths', globalData[i].NewDeaths],
                ['NewRecovered', globalData[i].NewRecovered],
                ['TotalConfirmed', globalData[i].TotalConfirmed],
                ['TotalDeaths', globalData[i].TotalDeaths],
                ['TotalRecovered', globalData[i].TotalRecovered]
            ]);
        }
        var options = {
            'title': 'Global Covid Data'
        };
        var chart = new google.visualization.PieChart(document.getElementById('ChartForGlobal'));
        chart.draw(data, options);
    };
}