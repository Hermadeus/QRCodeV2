using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using QRCode.Feedbacks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class FeedbackTest : SerializedMonoBehaviour
{
    [HideInInspector] public Sequencer Sequencer;
    [HideInInspector] public Feedback screenShake;
    public Image image;
    public CinemachineVirtualCamera cam;
    public ParticleSystem ps;
    public Image flash;


    private void Start()
    {
        transform.DOMoveY(transform.position.y + 1f, 1f).SetLoops(15000);
        Sequencer = FeedbackMaker.MakeSequence(this);
        Sequencer
            .Append(new FeedbackScreenFlash(this, image))
            .Append(new FeedbackScreenShake(this, cam))
            .Append(new FeedbackInstantiateParticleSystem(this, ps, transform))
            .Append(new FeedbackScreenZoom(this, cam))
            .Append(new FeedbackShaker(this, transform).SetDuration(.25f))
            .Append(new FeedbackScreenFieldOfView(this, cam))
            .Append(new FeedbackFreezeFrame(this))
            .UseDurationAsDelayAfter(true);
    }

    [Button]
    public void TestSequencer()
    {
        Sequencer.Play();
    }

    [Button]
    public void TestFreezeFrame()
    {
        var shake = FeedbackMaker.MakeFeedback<FeedbackFreezeFrame>(this).SetDuration(.1f);
    }

    [Button]
    public void TestScreenShake()
    {
        var freeze = FeedbackMaker.MakeFeedback<FeedbackScreenShake>(this);
        freeze.SetDuration(.2f).Play();
        freeze.camera = cam;
        freeze.Play();
    }

    [Button]
    public void TestShaker()
    {
        new FeedbackShaker(this, transform).SetDuration(2f).Play();
    }

    [Button]
    public void TestScreenZoom()
    {
        new FeedbackScreenZoom(this, cam, 2f).Play();
    }

    [Button]
    public void TestScreenFOV()
    {
        new FeedbackScreenFieldOfView(this, cam).Play();
    }

    [Button]
    public void TestScreenFlash()
    {
        new FeedbackScreenFlash(this, flash).Play();
    }
}
