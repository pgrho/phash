function output(digests, matches, perf, id) {
    id = id || 0;

    var sel = digests[id];

    if (sel) {
        document.querySelector("h1").innerText = sel.path;
        document.querySelector("#selectedImage").src = sel.url;
    }

	document.querySelector("#performanceReport").innerText = perf.report;

    var tbody = document.querySelector("tbody");
    tbody.innerHTML = "";

    var ms = matches.filter(m => m.i === id || m.j === id).map(m => ({ i: m.i === id ? m.j : m.i, ccr: m.ccr }));
    ms.sort((a, b) => b.ccr - a.ccr);

    for (var m of ms) {

        var h = (function (i) { return function (e) { output(digests, matches, i); e.preventDefault(); }; })(m.i);

        var od = digests[m.i];

        var tr = document.createElement("tr");

        var c1 = document.createElement("td");

        var a = document.createElement("a");
        a.href = od.url;
        a.onclick = h;

        var img = document.createElement("img");
        img.src = od.url;
        a.appendChild(img);

        c1.appendChild(a);

        tr.appendChild(c1);

        var c2 = document.createElement("td");

        var a = document.createElement("a");
        a.href = od.url;
        a.onclick = h;
        a.innerText = od.path;

        c2.appendChild(a);

        tr.appendChild(c2);

        var c3 = document.createElement("td");
        c3.innerText = m.ccr.toFixed(6);
        tr.appendChild(c3);

        tbody.appendChild(tr);
    }
}