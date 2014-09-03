﻿using UnityEngine;
using System.Collections;

public class DisconnectionNotice : MonoBehaviour {

  public HandController controller;
  public float fadeInTime = 1.0f;
  public float fadeOutTime = 1.0f;
  public AnimationCurve fade;
  public int waitFrames = 10;
  public Texture2D embeddedReplacementImage;

  private float fadedIn = 0.0f;
  private int frames_disconnected_ = 0;

  void Start() {
    SetAlpha(0.0f);
  }

  void SetAlpha(float alpha) {
    Color color = renderer.material.color;
    color.a = alpha;
    renderer.material.color = color;
  }

  void Update() {
    if (embeddedReplacementImage != null && controller.IsEmbedded()) {
      renderer.material.mainTexture = embeddedReplacementImage;
    }

    if (controller.IsConnected())
      frames_disconnected_ = 0;
    else
      frames_disconnected_++;

    if (frames_disconnected_ < waitFrames)
      fadedIn -= Time.deltaTime / fadeOutTime;
    else
      fadedIn += Time.deltaTime / fadeInTime;
    fadedIn = Mathf.Clamp(fadedIn, 0.0f, 1.0f);

    SetAlpha(fade.Evaluate(fadedIn));
  }
}
