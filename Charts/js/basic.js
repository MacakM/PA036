
$(document).ready(function(){     
  $(".chart").each(function(index,element){
    initializeChart($(element).attr("id"),data[$(element).attr('data-file')]);
    initializeAvg($(element).parent().find(".summary"),data[$(element).attr('data-file')])   
  });
});  

var initializeAvg = function(summaryElement,data){
  console.log(summaryElement);
  console.log(data.length);
  var avgCpu = 0;
  var avgRam = 0;
  var avgItems = 0;
  var avgHdd = 0;
  $(data).each(function(i,elem){
    avgCpu = avgCpu + parseFloat(elem.CpuUsage);
    avgRam = avgRam + parseFloat(elem.MemorySize); 
    avgItems = avgItems + parseFloat(elem.CacheEntriesCount); 
    avgHdd = avgHdd + parseFloat(elem.DiskUsage);    
  });
  avgCpu = avgCpu / data.length;
  avgRam = avgRam / data.length; 
  avgItems = avgItems / data.length; 
  avgHdd = avgHdd / data.length;
  
  $(summaryElement).find('.cpu-usage-avg').html(Math.round(avgCpu * 1000) / 1000);
  $(summaryElement).find('.ram-usage-avg').html(Math.round(avgRam * 1000) / 1000);
  $(summaryElement).find('.entries-count-avg').html(Math.round(avgItems));
  $(summaryElement).find('.disk-usage-avg').html(Math.round(avgHdd * 1000) / 1000);
   
}

var initializeChart = function(htmlElementId,data){
  AmCharts.makeChart(htmlElementId,
	{
		"type": "serial",
		"categoryField": "FormattedTime",
		"startDuration": 1,
		"decimalSeparator": ",",
		"thousandsSeparator": " ",
		"categoryAxis": {
			"gridPosition": "start",
			"titleFontSize": 2
		},
		"trendLines": [],
		"graphs": [
			{
				"bullet": "round",
				"fontSize": 7,
				"id": "AmGraph-1",
				"minDistance": 0,
				"title": "využití RAM (MB)",
				"type": "smoothedLine",
				"valueField": "MemorySize"
			},
			{
				"bullet": "square",
				"id": "AmGraph-2",
				"title": "Poèet záznamù",
				"type": "smoothedLine",
				"valueAxis": "CachedEntriesCount",
				"valueField": "CacheEntriesCount",
				"yAxis": "CacheEntriesCount"
			},
			{
				"bullet": "diamond",
				"id": "AmGraph-4",
				"title": "využití Cpu (%)",
				"type": "smoothedLine",
				"valueAxis": "CpuUsage",
				"valueField": "CpuUsage",
				"yAxis": "CpuUsage"
			},
			{
				"bullet": "diamond",
				"id": "AmGraph-5",
				"title": "využití disku (%)",
				"type": "smoothedLine",
				"valueAxis": "DiskUsage",
				"valueField": "DiskUsage",
				"yAxis": "DiskUsage"
			}
		],
		"guides": [],
		"valueAxes": [
			{
				"id": "MemorySize",
				"stackType": "regular",
				"axisColor": "#FF8000",
				"title": "",
				"titleColor": "#FF8000"
			},
			{
				"id": "CachedEntriesCount",
				"stackType": "regular",
				"axisColor": "#FFFF00",
				"offset": 40,
				"title": "",
				"titleColor": "#FFFF00"
			},
			{
				"id": "CpuUsage",
				"position": "right",
				"stackType": "regular",
				"axisColor": "#008000",
				"titleFontSize": 10
			},
			{
				"id": "DiskUsage",
				"position": "right", 
				"offset": 35,
				"stackType": "regular",
				"axisColor": "#0d8ecf",
				"titleFontSize": 10
			}
		],
		"allLabels": [],
		"balloon": {},
		"legend": {
			"enabled": true,
			"useGraphSettings": true
		},
		"dataProvider": data 
    }
	);
}
