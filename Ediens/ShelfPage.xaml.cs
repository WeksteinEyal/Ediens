using Newtonsoft.Json;
using System.Collections.Generic;

namespace Ediens;

public partial class ShelfPage : ContentPage
{
    private const string ListOfFoodsKey = "ListOfFoods";

    internal List<Food> _listOfFoods = new List<Food>();
    internal List<Food> ListOfFoods
    {
        get { return _listOfFoods; }
        set
        {
            _listOfFoods = value;
            DisplayFoodItems(ListOfFoods);
        }
    }

    public ShelfPage()
	{
		InitializeComponent();
        LoadListOfFoods();
    }

    private async void OnAddFoodClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddFood(this));
    }

    internal void DisplayFoodItems(List<Food> foods)
    {
        // Clear existing content
        ContentLayout.Children.Clear();

        var addButton = new Button { Text = "Add Food" };
        addButton.Clicked += OnAddFoodClicked;

        var grid = new Grid();
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }); // Auto size for first column
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) }); // Auto size for second column
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) }); // Auto size for third column
        int gridRowCount = 0;

        foreach (var food in foods)
        {
            grid.RowDefinitions.Add(new RowDefinition());
            var nameLabel = new Label { Text = $"Name: {food.Name}" };
            var portionTypeLabel = new Label { Text = $"Portion Type: {food.PortionType}" };
            var kcalLabel = new Label { Text = $"Kcal: {food.KcalPerPortion}" };
            var quantityLabel = new Label { Text = $"Quantity: {food.Quantity}" };

            var deleteButton = new Button { Text = "Delete" };
            deleteButton.Clicked += (sender, args) => OnDeleteFoodClicked(food);

            var modifyButton = new Button { Text = "Modify" };
            modifyButton.Clicked += (sender, args) => OnModifyFoodClicked(food);

            var layout = new HorizontalStackLayout();
            var layoutInfo = new StackLayout();

            layoutInfo.Children.Add(nameLabel);
            layoutInfo.Children.Add(portionTypeLabel);
            layoutInfo.Children.Add(kcalLabel);
            layoutInfo.Children.Add(quantityLabel);

            grid.Add(layoutInfo, 0, gridRowCount);
            grid.Add(modifyButton, 1, gridRowCount);
            grid.Add(deleteButton, 2, gridRowCount);

            gridRowCount += 1;
        }
        ContentLayout.Children.Add(grid);
        SaveListOfFoods(foods);
    }
    private void OnDeleteFoodClicked(Food food)
    {
        ListOfFoods.Remove(food);
        ListOfFoods = ListOfFoods; // Trigger UI update
    }

    private void OnModifyFoodClicked(Food food)
    {
        // Implement modify logic
    }

    private void SaveListOfFoods(List<Food> foods)
    {
        string serializedFoods = JsonConvert.SerializeObject(foods); // Serialize the list of foods to JSON
        Preferences.Set(ListOfFoodsKey, serializedFoods); // Save the serialized list to Preferences
    }

    private void LoadListOfFoods()
    {
        if (Preferences.ContainsKey(ListOfFoodsKey))
        {
            string serializedFoods = Preferences.Get(ListOfFoodsKey, string.Empty); // Get the serialized list from Preferences
            ListOfFoods = JsonConvert.DeserializeObject<List<Food>>(serializedFoods); // Deserialize the JSON to a list of foods
        }
    }
}