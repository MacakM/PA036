var expCount = 0;
var apiPort = 0;

$("#startButton").click(startTests);

function startTests()
{
    $("#startButton").attr("disabled", true);
    $("#resultsTable tbody").html("");

    expCount = $("#expCount").val();
    apiPort = $("#apiPort").val();

    runExperiment(1);
}

function runExperiment(id)
{
    if (id > expCount)
    {
        $("#statusText").html("Finished!");
        $("#startButton").attr("disabled", false);
        return;
    }
    $("#statusText").html("Running experiment " + id + "...");

    $.get("http://localhost:" + apiPort + "/api/BobTheBuilder/Experiment/" + id, function (data) {
        for (var r = 0; r < data.Results.length; r++)
            $("#resultsTable tbody").append(buildRow(id, r + 1, data.Results[r], null));
        runExperiment(id + 1);
    });
}

function buildRow(id, test, dotNet, php)
{
    var result = "<tr>";
    result += addCol(id);
    result += addCol(test);
    result += addCol(dotNet.Cached);
    result += addCol(dotNet.Time, "ms");
    result += addCol(Math.round(dotNet.Memory * 10000) / 10000, "MB");
    result += addCol(dotNet.EntryCount);
    result += addCol("-");
    result += addCol("-");
    result += addCol("-");
    result += addCol("-");
    result += "</tr>";
    return result;
}

function addCol(value, inf)
{
    inf = inf == undefined ? "" : inf;
    return "<td>" + value + " " + inf + "</td>";
}