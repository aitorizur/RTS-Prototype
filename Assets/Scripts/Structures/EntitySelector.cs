using System.Collections.Generic;

public class EntitySelector
{
    private List<ISelectableEntity> selectedEntities = new List<ISelectableEntity>();

    public void AddEntity(ISelectableEntity entity)
    {
        selectedEntities.Add(entity);
        entity.SelectEntity();
    }

    public void UnselectEntities()
    {
        UnSelectEntitiesIfNotNull();
        selectedEntities.Clear();
    }

    private void UnSelectEntitiesIfNotNull()
    {
        foreach (ISelectableEntity currentEntity in selectedEntities)
        {
            currentEntity.UnSelectEntity();
        }
    }
}
