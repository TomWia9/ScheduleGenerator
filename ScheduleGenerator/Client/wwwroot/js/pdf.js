function generatePdf(scheduleName) {

    const { jsPDF } = window.jspdf;
    const pdf = new jsPDF("p", "pt", "a4");

    pdf.html(document.getElementById("content"), {
        html2canvas: {
            scale: 0.488
        },
        callback: function (pdf) {
            pdf.save(scheduleName + ".pdf");
        }
    });

}