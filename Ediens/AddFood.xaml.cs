namespace Ediens;

public partial class AddFood : ContentPage

{
    private ShelfPage _shelfPage;
    public AddFood(ShelfPage shelfPage)
	{
		InitializeComponent();
        _shelfPage = shelfPage;
    }
    private void OnAddFoodClicked(object sender, EventArgs e)
    {
        if (new[] { NameEntry.Text, PortionTypeEntry.Text, KcalEntry.Text, QuantityEntry.Text }.Any(string.IsNullOrWhiteSpace))
        {
            return;
        }

        Food newFood = new Food(
                NameEntry.Text,
                PortionTypeEntry.Text,
                float.Parse(KcalEntry.Text),
                int.Parse(QuantityEntry.Text)
            );

        // Add the new Food object to the ListOfFoods in ShelfPage
        _shelfPage.ListOfFoods.Add(newFood);
        _shelfPage.ListOfFoods = _shelfPage.ListOfFoods;

        // Navigate back to the ShelfPage
        Navigation.PopAsync();
    }
}