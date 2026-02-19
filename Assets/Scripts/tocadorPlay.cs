using UnityEngine;

public class tocadorPlay : MonoBehaviour
{
   AudioSource audioSource;

   public void playPlay(){
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
   }
}
