public struct Affinity
{
    public Affinity(float rage, float fortitude, float divination) {
        this.rage = rage;
        this.fortitude = fortitude;
        this.divination = divination;
    }

    private float rage;
    private float fortitude;
    private float divination;
}

enum AffinityEnum {
    Rage,
    Fortitude,
    Divination,
}