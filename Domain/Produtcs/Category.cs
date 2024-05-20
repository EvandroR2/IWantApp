using Flunt.Validations;
using System.Diagnostics.Contracts;

namespace IWantApp.Domain.Produtcs;

public class Category : Entity
{

    public string Name { get; private set; }
    public bool Active { get; private set; }
    public string CreateBy { get; private set; }
    public string EditeBy { get; private set; }
    public DateTime CreateOn { get; private set; }
    public DateTime EditeOn { get; private set; }

    // Construtor padrão sem parâmetros
    protected Category()
    {
        // Este construtor vazio é necessário pelo Entity Framework
    }

    public Category(string name, string createby, string editeby)
    {
        Name = name;
        Active = true;
        CreateBy = createby;
        EditeBy = editeby;
        CreateOn = DateTime.Now;
        EditeOn = DateTime.Now;

        Validate();

    }

    private void Validate()
    {
        var contract = new Contract<Category>()
            .IsNotNullOrEmpty(Name, "Name", "Nome Obrigatorio")
            .IsGreaterOrEqualsThan(Name, 3, "Name")
            .IsNotNullOrEmpty(CreateBy, "CreateBy")
            .IsNotNullOrEmpty(EditeBy, "EditeOn");
        AddNotifications(contract);
    }

    public void EditInfo(string name,bool active, string editeBy) 
    { 
        Active = active;
        Name = name;
        EditeBy = editeBy;
        EditeOn = DateTime.Now;

        

        Validate();
    }


}
