function generatePdf(scheduleName) {

    const { jsPDF } = window.jspdf;

    const pdf = new jsPDF("l", "pt", "a4");

    console.log(document.getElementById("content"));

    pdf.html(document.getElementById("content"), {
        callback: function (pdf) {
            pdf.save(scheduleName + ".pdf");
        }
    });
}