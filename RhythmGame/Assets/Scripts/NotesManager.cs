using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesManager : MonoBehaviour
{
    public List<float> notesPosZ = new List<float>();
    public float speed = 7f;
    public float maxZ = 1000f;
    public GameObject noteParticlePre;
    public GameObject skullParticlePre;
    public GameObject resultPanel;
    public GameObject skullHead;
    private List<Note> allNotes = new List<Note>();

    void Awake()
    {
        if (Data.notes.Count == 0)
        {
            AudioClip noteDo = Resources.Load("piano-mp3/C3") as AudioClip;
            AudioClip noteRea = Resources.Load("piano-mp3/D3") as AudioClip;
            AudioClip noteMi = Resources.Load("piano-mp3/E3") as AudioClip;
            AudioClip notePa = Resources.Load("piano-mp3/F3") as AudioClip;
            AudioClip noteSol = Resources.Load("piano-mp3/G3") as AudioClip;
            AudioClip noteLa = Resources.Load("piano-mp3/A3") as AudioClip;
            AudioClip noteSi = Resources.Load("piano-mp3/B3") as AudioClip;
            AudioClip noteDoHigh = Resources.Load("piano-mp3/C4") as AudioClip;
            AudioClip noteReaHigh = Resources.Load("piano-mp3/D4") as AudioClip;
            AudioClip noteMiHigh = Resources.Load("piano-mp3/E4") as AudioClip;
            AudioClip notePaHigh = Resources.Load("piano-mp3/F4") as AudioClip;
            AudioClip noteSolHigh = Resources.Load("piano-mp3/G4") as AudioClip;
            AudioClip noteSiLow = Resources.Load("piano-mp3/B2") as AudioClip;
            AudioClip noteLaLow = Resources.Load("piano-mp3/A2") as AudioClip;
            AudioClip noteSolLow = Resources.Load("piano-mp3/G2") as AudioClip;
            AudioClip noteReaHB = Resources.Load("piano-mp3/Db4") as AudioClip;
            AudioClip noteSiB = Resources.Load("piano-mp3/Bb3") as AudioClip;
            Data.notes.Add(noteDo);
            Data.notes.Add(noteRea);
            Data.notes.Add(noteMi);
            Data.notes.Add(notePa);
            Data.notes.Add(noteSol);
            Data.notes.Add(noteLa);
            Data.notes.Add(noteSi);
            Data.notes.Add(noteDoHigh);
            Data.notes.Add(noteReaHigh);
            Data.notes.Add(noteMiHigh);
            Data.notes.Add(notePaHigh);
            Data.notes.Add(noteSolHigh);
            Data.notes.Add(noteSiLow);
            Data.notes.Add(noteLaLow);
            Data.notes.Add(noteSolLow);
            Data.notes.Add(noteReaHB);
            Data.notes.Add(noteSiB);
        }
        // PositionNotes();
    }

    public void PositionNotes()
    {
        transform.localPosition = new Vector3(1000, 0, -100f);
        foreach (Skull skullH in GetComponentsInChildren<Skull>())
        {
            Destroy(skullH.transform.parent.gameObject);
        }

        int i = 0;
        GameObject preNote = null;
        GameObject preSkull = null;
        if (allNotes.Count != 0)
        {
            foreach (Note note in allNotes)
                note.gameObject.SetActive(true);
        }
        foreach (Note note in GetComponentsInChildren<Note>())
        {
            if (!allNotes.Contains(note))
                allNotes.Add(note);

            bool isSkullHead = Random.Range(0, 5) > 2;
            int rndX = Random.Range(-2, 2) * 10;
            if (preNote != null && Mathf.Abs(notesPosZ[i] - notesPosZ[i - 1]) < 7f)
            {
                switch (preNote.transform.localPosition.x)
                {
                    case -20:
                        rndX = Random.Range(-2, -1) * 10;
                        break;
                    case -10:
                        rndX = Random.Range(-2, 0) * 10;
                        break;
                    case 0:
                        rndX = Random.Range(-1, 1) * 10;
                        break;
                    case 10:
                        rndX = Random.Range(0, 2) * 10;
                        break;
                    case 20:
                        rndX = Random.Range(1, 2) * 10;
                        break;
                }
            }
            note.transform.parent.localPosition = new Vector3(rndX, note.transform.parent.localPosition.y, notesPosZ[i]);
            preNote = note.transform.parent.gameObject;

            if (isSkullHead && preSkull != null && Mathf.Abs(preNote.transform.localPosition.z - preSkull.transform.localPosition.z) > 5f || isSkullHead && preSkull == null)
            {
                GameObject skull = Instantiate(skullHead);
                skull.transform.parent = this.transform;
                float x = Random.Range(-2, 2) * 10;
                if (x == rndX && rndX == 20) x = -20;
                else if (x == rndX && rndX == -20) x = 20;
                else if (x == rndX) x = rndX + 10;
                x -= 2;
                skull.transform.localPosition = new Vector3(x, preNote.transform.localPosition.y + 1, preNote.transform.localPosition.z);
                preSkull = skull.transform.parent.gameObject;
            }

            i++;
        }
    }

    void Update()
    {
        if (Data.isGame)
            transform.position += Vector3.forward * speed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Backspace))
            PositionNotes();

        if (Data.isGame && this.transform.position.z > maxZ)
        {
            Data.isGame = false;
            QuestManager.tutorialOK = false;
            GameObject.Find("Player").GetComponent<PlayerMove>().ExitGame();
        }
        else if (Data.isGame && this.transform.position.z > maxZ - 50 && !resultPanel.activeSelf)
        {
            resultPanel.SetActive(true);
        }
    }
}
