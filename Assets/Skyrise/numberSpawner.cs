using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class numberSpawner : MonoBehaviour
{
    public GameObject prefab;

    public Vector2[] bounds;
    public Vector2Int numberRange;
    public int count;
    public float minDistance = 1.5f;

    public float timeToShow = 1f;
    bool showing = false;

    float timeState = 0;
    public int[] currentIntegers;
    int step;
    bool inGame = false;
    int spawned;

    public GameObject continueButton;

    List<number> numberObjects = new List<number>();

    // Start is called before the first frame update
    void Start()
    {
        step = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (showing)
        {
            timeState += Time.deltaTime;
            if(timeToShow < timeState)
            {
                showing = false;
                timeState = 0;
                hideNumbers();
            }
        }
    }

    void spawnNumbers(Vector2Int numberRange, int count)
    {
        updateDifficulty();

        spawned = count;
        currentIntegers = getList(numberRange, count);
        for (int i = 0; i < count; i++)
        {
            number n = Instantiate(prefab).GetComponent<number>();
            numberObjects.Add(n);
            n.numberAssigned = currentIntegers[i];

            float posX = Random.Range(bounds[0].x, bounds[1].x);
            float posY = Random.Range(bounds[0].y, bounds[1].y);
            n.transform.position = new Vector2(posX, posY);

            bool tooClose = false;
            if (numberObjects.ToArray().Length >= 1)
            {
                for (int j = 0; j < 16; j++)
                {
                    tooClose = false;
                    foreach (number num in numberObjects)
                    {
                        
                        if (Vector2.Distance(n.transform.position, num.transform.position) < minDistance && n != num)
                        {
                            tooClose = true;
                        }
                    }
                    Debug.Log(tooClose);
                    if (tooClose)
                    {
                        posX = Random.Range(bounds[0].x, bounds[1].x);
                        posY = Random.Range(bounds[0].y, bounds[1].y);
                        n.transform.position = new Vector2(posX, posY);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            n.showNumbers();

        }
    }

    void updateDifficulty()
    {
        float score = (float)FindAnyObjectByType<scoreManagerSkyRise>().scoreNum / 1250;
        numberRange.x = Mathf.RoundToInt(score / 10);
        numberRange.y = Mathf.RoundToInt(score / 3) + 10;
        
    }

    void hideNumbers()
    {
        foreach(number n in numberObjects)
        {
            n.hideNumbers();
        }
    }

    int[] getList(Vector2Int numberRange, int count)
    {
        List<int> randomNumbers = new List<int>();

        for (int i = 0; i < count; i++)
        {
            int number;

            do
            {
                number = Random.Range(numberRange.x, numberRange.y);
            } while (randomNumbers.Contains(number));

            randomNumbers.Add(number);
        }
        randomNumbers.Sort();
        return randomNumbers.ToArray();
    }
    public void continueBut()
    {
        if (!inGame)
        {
            showing = true;
            spawnNumbers(numberRange, count);
            step = 0;
            inGame = true;
        }
    }
    public void numberClicked(number number)
    {
        if (!showing)
        {
            if (number.numberAssigned == currentIntegers[step])
            {
                number.anim.SetTrigger("flip");
                number.disabled = true;
                step++;
                if (step >= currentIntegers.Length)
                {
                    FindObjectOfType<backgroundMover>().moveUp(1.5f);
                    FindAnyObjectByType<scoreManagerSkyRise>().updateScore(1250);
                    resetGame();
                }
            }
            else
            {
                FindAnyObjectByType<scoreManagerSkyRise>().updateLives();
                resetGame();
            }
        }
    }
    void resetGame()
    {
        foreach(number n in numberObjects)
        {
            n.anim.SetTrigger("flip");
            n.anim.SetTrigger("close");
        }
        numberObjects.Clear();
        

    }
    public void numberDestroyed()
    {
        spawned -= 1;
        if(spawned <= 0)
        {
            continueButton.GetComponent<continueButton>().anim.SetTrigger("appear");
            inGame = false;

        }
    }

}
