using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SimpleAnimInstance
{
    [SerializeField] protected SimpleAnim _simpleAnim = null;

    public SimpleAnim Instance
    {
        get
        {
            if(_simpleAnimInstance == null && _simpleAnim != null)
            {
                _simpleAnimInstance = _simpleAnim.Instantiate();
            }
            return _simpleAnimInstance;
        }
    }

    protected SimpleAnim _simpleAnimInstance = null;
}

[Serializable]
public class SimpleAnimCurve
{
    public AnimationCurve Curve = new AnimationCurve();
    public float          Value = 1.0f;

    public float Evaluate(float time)
    {
        return Curve.Evaluate(time) * Value;
    }
}

[CreateAssetMenu(fileName = "SimpleAnim", menuName = "SimpleAnim")]
public class SimpleAnim : AncestorScriptable
{
    public delegate void OnAnimEndDelegate(SimpleAnim anim);
    public event OnAnimEndDelegate OnAnimEnd;

    public enum EState
    {
        None,
        Playing,
        Pause,
        Stopped
    }

    public float Duration = 1.0f;
    public float Value    = 1.0f;
    public bool  Loop     = false;

    public bool Reversed { get; protected set; }

    public AnimationCurve AnimationCurve  = new AnimationCurve();

    public List<SimpleAnimCurve> AnimationCurves = new List<SimpleAnimCurve>();

    public float  ElapsedTime { get; protected set; }
    public EState State       { get; protected set; }

    public float Evaluate(int i = 0)
    {
        float result = 0.0f;
        float time   = ElapsedTime / Duration;

        if(Reversed)
        {
            time = 1.0f - time;
        }

        if (i == 0)
        {
            result = AnimationCurve.Evaluate(time) * Value;
        }
        else
        {
            result = AnimationCurves[i - 1].Evaluate(time);
        }
        return result;
    }

    public virtual void StopReversed()
    {
        Reversed = true;
        Stop();
    }

    public virtual void Stop()
    {
        ElapsedTime = Duration;
        State       = EState.Stopped;

        if (OnAnimEnd != null)
        {
            OnAnimEnd(this);
        }
    }

    public virtual void Play()
    {
        ElapsedTime = 0.0f;
        State       = EState.Playing;
        Reversed    = false;
    }

    public virtual void PlayReversed()
    {
        Play();
        Reversed = true;
    }

    public virtual float Tick(float deltaTime)
    {
        if(State == EState.Playing)
        {
            ElapsedTime += deltaTime;
            if(ElapsedTime >= Duration)
            {
                if(Loop)
                {
                    while(ElapsedTime >= Duration)
                    {
                        ElapsedTime -= Duration;
                    }                    
                }
                else
                {
                    Stop();
                }
            }
        }
        return Evaluate();
    }

    public SimpleAnim Instantiate()
    {
        return UnityEngine.Object.Instantiate(this) as SimpleAnim;
    }
}
