[System.Serializable]
public class LocalizationData
{
    public LocalizationItem[] items;
}

[System.Serializable]
public class LocalizationItem
{
    public string key;
    public string value;
}

[System.Serializable]
public class ImportRecipe
{
    public Recipe[] recipes;
}

[System.Serializable]
public class Recipe
{
    public string name;
    public RecipeData[] recipeData;

}

[System.Serializable]
public class RecipeData
{
    public int count;
    public string edible;
}


