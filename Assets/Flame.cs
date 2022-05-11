using UnityEngine;
using UnityEngine.VFX;

public class Flame : Skill
{
    public VisualEffect flame;


    // Start is called before the first frame update
    void Start()
    {
        flame.Stop();
    }

    public void FlameOn()
    {
        flame.Play();
    }

    public void FlameOff()
    {
        flame.Stop();
    }


}
