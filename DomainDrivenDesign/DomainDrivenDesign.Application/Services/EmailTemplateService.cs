namespace DomainDrivenDesign.Application.Services;
public static class EmailTemplateService
{
    public static string CreateRegisterBody(string email)
    {
        string body = @$"<h1>Merhaba!</h1>

    Uygulamamıza başarıyla kayıt oldunuz. Aşağıdaki linki tıklayarak mail adresinizi onaylamanız gerekmektedir.


<a href=""https://localhost:7108/api/Auth/ConfirmEmail?email={email}"" target=""_blank"">Mail adresini onayla</a>";

        return body;
    }

    public static string CreateAfterConfirmEmailBody()
    {
        string body = @$"<h1>Mail adresiniz onaylandı</h1>

    Uygulamamıza giriş yaparak keşfetmeye başlayabilirsiniz.

<a href=""http://localhost/4200/login"" target=""_blank"">Uygulama giriş yap</a>";

        return body;
    }
}
