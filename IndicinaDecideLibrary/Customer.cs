namespace IndicinaDecideLibrary;

public class Customer
{
    public string customer_id { get; set; }
    public string? email { get; set; }
    public string? first_name { get; set; }
    public string? last_name { get; set; }
    public string? phone { get; set; }

    public Customer(string customer_id, string email, string first_name, string last_name, string phone)
    {
        this.customer_id = customer_id;
        this.email = email;
        this.first_name = first_name;
        this.last_name = last_name;
        this.phone = phone;
    }

    public Dictionary<string, object> info
    {
        get
        {
            var dictionary = new Dictionary<string, object>
            {
                { "id", this.customer_id },
            };

            if (this.email != null)
                dictionary.Add("email", this.email);
            
            if (this.first_name != null)
                dictionary.Add("firstName", this.first_name);
            
            if (this.last_name != null)
                dictionary.Add("lastName", this.last_name);
            
            if (this.phone != null)
                dictionary.Add("phone", this.phone);

            return dictionary;
        }
    }

}
