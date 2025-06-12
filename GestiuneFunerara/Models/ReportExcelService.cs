using System;
using System.IO;
using System.Drawing;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using GestiuneFunerara.Models;

public class ReportExcelService
{
    private readonly GestiuneFuneraraContext _db;

    public ReportExcelService(GestiuneFuneraraContext db)
    {
        _db = db;
    }

    public async Task<byte[]> GeneratePrecomenziExcel()
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        var precomenzi = await _db.Precomenzis
            .Include(p => p.id_utilizatorNavigation)
            .Include(p => p.id_pachetefunerareNavigation)
            .ToListAsync();

        using var package = new ExcelPackage();
        var worksheet = package.Workbook.Worksheets.Add("Raport Precomenzi");

        var headers = new[]
        {
        "ID", "Nume Utilizator", "Pachet", "Nume Defunct", "Data Deces",
        "Locație Ridicare", "Observații", "Stare", "Data Creare"
    };

        for (int i = 0; i < headers.Length; i++)
        {
            worksheet.Cells[1, i + 1].Value = headers[i];
            worksheet.Cells[1, i + 1].Style.Font.Bold = true;
            worksheet.Cells[1, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            worksheet.Cells[1, i + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
        }

        int row = 2;
        foreach (var p in precomenzi)
        {
            worksheet.Cells[row, 1].Value = p.id_precomenzi;
            worksheet.Cells[row, 2].Value = p.id_utilizatorNavigation?.NumeComplet ?? "—";
            worksheet.Cells[row, 3].Value = p.id_pachetefunerareNavigation?.Denumire ?? "—";
            worksheet.Cells[row, 4].Value = p.NumeDefunct ?? "—";
            worksheet.Cells[row, 5].Value = p.DataDeces?.ToString("dd/MM/yyyy") ?? "—";
            worksheet.Cells[row, 6].Value = p.LocatieRidicare ?? "—";
            worksheet.Cells[row, 7].Value = p.Observatii ?? "—";
            worksheet.Cells[row, 8].Value = p.Stare ?? "—";
            worksheet.Cells[row, 9].Value = p.DataCreare.ToString("dd/MM/yyyy HH:mm");
            row++;
        }

        worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

        // data generarii
        worksheet.Cells[row + 2, 1].Value = $"Raport generat la: {DateTime.Now:dd/MM/yyyy HH:mm}";
        worksheet.Cells[row + 2, 1, row + 2, headers.Length].Merge = true;
        worksheet.Cells[row + 2, 1].Style.Font.Italic = true;

        return package.GetAsByteArray();
    }


    public async Task<byte[]> GenerateUtilizatoriExcel()
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        var utilizatori = await _db.Utilizatoris.ToListAsync();

        using var package = new ExcelPackage();
        var worksheet = package.Workbook.Worksheets.Add("Raport Utilizatori");

        var headers = new[]
        {
        "ID", "Nume Complet", "Email", "Telefon", "Rol", "Data Înregistrare"
    };

        for (int i = 0; i < headers.Length; i++)
        {
            worksheet.Cells[1, i + 1].Value = headers[i];
            worksheet.Cells[1, i + 1].Style.Font.Bold = true;
            worksheet.Cells[1, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            worksheet.Cells[1, i + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
        }

        int row = 2;
        foreach (var u in utilizatori)
        {
            worksheet.Cells[row, 1].Value = u.id_utilizator;
            worksheet.Cells[row, 2].Value = u.NumeComplet ?? "—";
            worksheet.Cells[row, 3].Value = u.Email ?? "—";
            worksheet.Cells[row, 4].Value = u.Telefon ?? "—";
            worksheet.Cells[row, 5].Value = u.Rol ?? "—";
            worksheet.Cells[row, 6].Value = u.DataInregistrare.ToString("dd/MM/yyyy");
            row++;
        }

        worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

        // data si ora generarii
        worksheet.Cells[row + 2, 1].Value = $"Raport generat la: {DateTime.Now:dd/MM/yyyy HH:mm}";
        worksheet.Cells[row + 2, 1, row + 2, headers.Length].Merge = true;
        worksheet.Cells[row + 2, 1].Style.Font.Italic = true;

        return package.GetAsByteArray();
    }


    public async Task<byte[]> GenerateStocuriSicrieExcel()
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using var package = new ExcelPackage();
        var worksheet = package.Workbook.Worksheets.Add("Stocuri Sicrie");

        // antet
        var headers = new[]
        {
        "ID", "Nume", "Descriere", "Preț Achiziție", "Preț Vânzare", "Stoc Curent", "Este Personalizat"
    };

        for (int i = 0; i < headers.Length; i++)
        {
            worksheet.Cells[1, i + 1].Value = headers[i];
            worksheet.Cells[1, i + 1].Style.Font.Bold = true;
            worksheet.Cells[1, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(Color.LightGreen);
            worksheet.Cells[1, i + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
        }

        // date
        var produse = await _db.Produses.ToListAsync();

        int row = 2;
        foreach (var p in produse)
        {
            worksheet.Cells[row, 1].Value = p.id_produs;
            worksheet.Cells[row, 2].Value = p.nume;
            worksheet.Cells[row, 3].Value = p.descriere;
            worksheet.Cells[row, 4].Value = p.pret_achizitie;
            worksheet.Cells[row, 5].Value = p.pret_vanzare;
            worksheet.Cells[row, 6].Value = p.stoc_curent;
            worksheet.Cells[row, 7].Value = p.este_personalizat ? "Da" : "Nu";
            row++;
        }

        worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

        // data si ora generarii raportului - adaugare
        worksheet.Cells[row + 2, 1].Value = $"Raport generat la: {DateTime.Now:dd/MM/yyyy HH:mm}";
        worksheet.Cells[row + 2, 1, row + 2, headers.Length].Merge = true;
        worksheet.Cells[row + 2, 1].Style.Font.Italic = true;

        return package.GetAsByteArray();
    }

}
