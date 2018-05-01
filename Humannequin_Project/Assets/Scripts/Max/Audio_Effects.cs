// AUDIO_EFFECTS.CS
// MAX MILLS

// This holds audio fade in/out methods

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Effects {

	public static IEnumerator AudioFadeOut(AudioSource source,float fade_time)
	{
		float startVolume = source.volume;

		while (source.volume > 0) {
			source.volume -= startVolume * Time.deltaTime / fade_time;

			yield return null;
		}

		source.Stop ();
		source.volume = startVolume;

		yield return null;
	}

	public static IEnumerator AudioFadeIn(AudioSource source,float fade_time)
	{
		float startVolume = source.volume;
		source.Play();
		source.volume = 0.0f;

		while (source.volume < 1) 
		{
			source.volume += startVolume * Time.deltaTime / fade_time;

			yield return null;
		}

		source.volume = startVolume;

		yield return null;
	}
}
