using System;
using System.Threading.Tasks;
using UnityEngine;

public class Timer
{
    private float time;
    private Action action;
    private bool isLoop;
    private float currentTime = 0f;

    public Timer(float time, Action action, bool isLoop = false)
    {
        this.time = time;
        this.action = action;
        this.isLoop = isLoop;
    }

    public async void Start()
    {
        await Update();
    }

    public void Reset()
    {
        currentTime = 0f;
    }

    public void Stop()
    {
        isLoop = false;
    }

    private async Task Update()
    {
        while (true)
        {
            currentTime += Time.deltaTime;

            await Task.Yield();

            if (currentTime >= time)
            {
                action?.Invoke();
                if (!isLoop)
                {
                    return;
                }
                Reset();
            }
        }
    }
}
