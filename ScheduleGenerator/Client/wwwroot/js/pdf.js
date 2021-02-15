function generatePdf(scheduleName, text) {

    const { jsPDF } = window.jspdf;

    const doc = new jsPDF();

    doc.text(text, 10, 10);

    doc.save(scheduleName + ".pdf");
}

