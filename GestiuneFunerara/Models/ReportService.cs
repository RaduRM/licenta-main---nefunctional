using GestiuneFunerara.Models;
using Microsoft.EntityFrameworkCore;
using QuestPDF;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Threading.Tasks;

public class ReportService
{
    private readonly GestiuneFuneraraContext _db;

    public ReportService(GestiuneFuneraraContext db)
    {
        _db = db;
    }

    public async Task<byte[]> GeneratePrecomenziPdf()
    {
        Settings.License = LicenseType.Community;

        var precomenzi = await _db.Precomenzis
            .Include(p => p.id_utilizatorNavigation)
            .Include(p => p.id_pachetefunerareNavigation)
            .ToListAsync();

        var currentDateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm");


        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(20);

                page.Header()
                .Column(column =>
                {
                    column.Item().Text("Raport Precomenzi").FontSize(20).Bold().FontColor(Colors.Blue.Medium);
                    column.Item().Text($"Generat pe {currentDateTime}").FontSize(10).FontColor(Colors.Grey.Darken1);
                });
                page.Content()
                    .Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(40);  // ID
                            columns.RelativeColumn(3);   // Nume Utilizator
                            columns.RelativeColumn(3);   // Pachet
                            columns.RelativeColumn(3);   // Nume Defunct
                            columns.RelativeColumn(2);   // Data Deces
                            columns.RelativeColumn(3);   // Locatie Ridicare
                            columns.RelativeColumn(4);   // Observatii
                            columns.RelativeColumn(2);   // Stare
                            columns.RelativeColumn(2);   // Data Creare
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(CellStyle).Text("ID");
                            header.Cell().Element(CellStyle).Text("Nume Utilizator");
                            header.Cell().Element(CellStyle).Text("Pachet");
                            header.Cell().Element(CellStyle).Text("Nume Defunct");
                            header.Cell().Element(CellStyle).Text("Data Deces");
                            header.Cell().Element(CellStyle).Text("Locație Ridicare");
                            header.Cell().Element(CellStyle).Text("Observații");
                            header.Cell().Element(CellStyle).Text("Stare");
                            header.Cell().Element(CellStyle).Text("Data Creare");
                        });

                        foreach (var p in precomenzi)
                        {
                            table.Cell().Element(CellStyle).Text(p.id_precomenzi.ToString());
                            table.Cell().Element(CellStyle).Text(p.id_utilizatorNavigation?.NumeComplet ?? "—");
                            table.Cell().Element(CellStyle).Text(p.id_pachetefunerareNavigation?.Denumire ?? "—");
                            table.Cell().Element(CellStyle).Text(p.NumeDefunct ?? "—");
                            table.Cell().Element(CellStyle).Text(p.DataDeces?.ToString("dd/MM/yyyy") ?? "—");
                            table.Cell().Element(CellStyle).Text(p.LocatieRidicare ?? "—");
                            table.Cell().Element(CellStyle).Text(p.Observatii ?? "—");
                            table.Cell().Element(CellStyle).Text(p.Stare ?? "—");
                            table.Cell().Element(CellStyle).Text(p.DataCreare.ToString("dd/MM/yyyy HH:mm"));
                        }
                    });
            });
        });

        return document.GeneratePdf();
    }

    public async Task<byte[]> GenerateUtilizatoriPdf()
    {
        Settings.License = LicenseType.Community;

        var utilizatori = await _db.Utilizatoris.ToListAsync();

        var currentDateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(20);

                page.Header()
                 .Column(column =>
                 {
                     column.Item().Text("Raport Utilizatori").FontSize(20).Bold().FontColor(Colors.Blue.Medium);
                     column.Item().Text($"Generat pe {currentDateTime}").FontSize(10).FontColor(Colors.Grey.Darken1);
                 });

                page.Content()
                    .Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(40);  // ID
                            columns.RelativeColumn(3);   // Nume Complet
                            columns.RelativeColumn(3);   // Email
                            columns.RelativeColumn(2);   // Telefon
                            columns.RelativeColumn(2);   // Rol
                            columns.RelativeColumn(2);   // Data Înregistrare
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(CellStyle).Text("ID");
                            header.Cell().Element(CellStyle).Text("Nume Complet");
                            header.Cell().Element(CellStyle).Text("Email");
                            header.Cell().Element(CellStyle).Text("Telefon");
                            header.Cell().Element(CellStyle).Text("Rol");
                            header.Cell().Element(CellStyle).Text("Data Înregistrare");
                        });

                        foreach (var u in utilizatori)
                        {
                            table.Cell().Element(CellStyle).Text(u.id_utilizator.ToString());
                            table.Cell().Element(CellStyle).Text(u.NumeComplet ?? "—");
                            table.Cell().Element(CellStyle).Text(u.Email ?? "—");
                            table.Cell().Element(CellStyle).Text(u.Telefon ?? "—");
                            table.Cell().Element(CellStyle).Text(u.Rol ?? "—");
                            table.Cell().Element(CellStyle).Text(u.DataInregistrare.ToString("dd/MM/yyyy"));
                        }
                    });
            });
        });

        return document.GeneratePdf();
    }
    public async Task<byte[]> GenerateRaportPrecomandaPdf(int idPrecomanda)
    {
        QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

        var precomanda = await _db.Precomenzis
            .Include(p => p.id_utilizatorNavigation)
            .Include(p => p.id_pachetefunerareNavigation)
            .Include(p => p.id_produsNavigation)
            .FirstOrDefaultAsync(p => p.id_precomenzi == idPrecomanda);

        if (precomanda == null)
            return new byte[0];


        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(30);

                page.Header()
                    .Column(header =>
                    {
                        header.Item().Text("Memorial SRL")
                            .FontSize(20).Bold().FontColor(Colors.Blue.Medium);

                        header.Item().Text($"Raport Precomandă #{precomanda.id_precomenzi}")
                            .FontSize(14).Bold();

                        header.Item().Text($"Generat pe: {DateTime.Now:dd/MM/yyyy HH:mm}")
                            .FontSize(10).Italic().FontColor(Colors.Grey.Darken1);
                    });

                page.Content().Column(col =>
                {
                    col.Spacing(10);

                    col.Item().PaddingTop(10).Text("📄 Detalii Client")
                        .FontSize(14).Bold().FontColor(Colors.Black);

                    col.Item().Text($"Nume complet: {precomanda.id_utilizatorNavigation.NumeComplet}");
                    col.Item().Text($"Telefon: {precomanda.id_utilizatorNavigation.Telefon}");
                    col.Item().Text($"Email: {precomanda.id_utilizatorNavigation.Email}");

                    col.Item().PaddingTop(10).Text("⚰️ Detalii Precomandă")
                        .FontSize(14).Bold().FontColor(Colors.Black);

                    col.Item().Text($"Nume defunct: {precomanda.NumeDefunct}");
                    col.Item().Text($"Data deces: {precomanda.DataDeces?.ToString("dd/MM/yyyy") ?? "—"}");
                    col.Item().Text($"Locație ridicare: {precomanda.LocatieRidicare}");

                    col.Item().Text($"Pachet funerar: {precomanda.id_pachetefunerareNavigation.Denumire}");
                    col.Item().Text($"Sicriu ales: {precomanda.id_produsNavigation?.nume ?? "—"}");
                    col.Item().Text($"Observații: {precomanda.Observatii ?? "—"}");

                    col.Item().PaddingTop(10).Text($"Data creare precomandă: {precomanda.DataCreare:dd/MM/yyyy HH:mm}")
                        .FontSize(10).Italic().FontColor(Colors.Grey.Darken1);
                });

                page.Footer()
                    .AlignCenter()
                    .Text("Memorial SRL - www.memorial.ro | contact@memorial.ro")
                    .FontSize(10)
                    .FontColor(Colors.Grey.Darken1);
            });
        });

        return document.GeneratePdf();
    }


    private IContainer CellStyle(IContainer container)
    {
        return container
            .Padding(5)
            .BorderBottom(1)
            .BorderColor(Colors.Grey.Lighten2);
    }
}
