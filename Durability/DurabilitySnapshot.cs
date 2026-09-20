namespace Landoria.HammerFreedom
{
    // Stores item durability so a patch can restore it after an action.
    internal struct DurabilitySnapshot
    {
        private readonly ItemDrop.ItemData _item;
        private readonly float _durability;

        // Captures an item only when durability must be preserved.
        internal DurabilitySnapshot(ItemDrop.ItemData item, bool preserve)
        {
            _item = preserve ? item : null;
            _durability = item?.m_durability ?? 0f;
        }

        // Restores the captured durability value.
        internal void Restore()
        {
            if (_item != null)
            {
                _item.m_durability = _durability;
            }
        }
    }
}
