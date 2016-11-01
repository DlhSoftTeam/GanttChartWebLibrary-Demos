function exportPngImage(ganttChartView) {
    // Get Project XML content.
    var projectXmlSerializerSettings = { compact: true };
    var projectSerializer = DlhSoft.Controls.GanttChartView.ProjectSerializer.initialize(ganttChartView, projectXmlSerializerSettings);
    var projectXml = projectSerializer.getXml();
    // Encode Project XML content to pass security requirements.
    projectXml = encodeURIComponent(projectXml);
    // Prepare HTML form and post Project XML content to server side PNG image exporter Web Form (see GetPng.aspx.cs).
    var form = document.createElement('form');
    form.setAttribute('method', 'POST');
    form.setAttribute('action', 'GetPng.aspx?theme=' + theme);
    var projectXmlField = document.createElement('input');
    projectXmlField.setAttribute('type', 'hidden');
    projectXmlField.setAttribute('name', 'ProjectXml');
    projectXmlField.setAttribute('value', projectXml);
    form.appendChild(projectXmlField);
    document.body.appendChild(form);
    form.submit();
    document.body.removeChild(form);
}
