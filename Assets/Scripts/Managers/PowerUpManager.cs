using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField] private Transform[] _positions;
    [SerializeField] private GameObject[] _cards;

    private List<int> cardSlots = new List<int>();
    private List<GameObject> activeCards = new List<GameObject>(); // Track instantiated cards
    private int randomCardIndex;
    private GameManager _gameManager;
    private UIManager _uiManager;
    public static PowerUpManager instance;

    public int RandomCardIndex { get => randomCardIndex; set => randomCardIndex = value; }
    public List<GameObject> ActiveCards { get => activeCards; set => activeCards = value; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        _gameManager = GameManager.instance;
        _uiManager = UIManager.instance;
    }

    public void SpawnCards()
    {
        cardSlots.Clear();
        ActiveCards.Clear();

        for (int i = 0; i < _positions.Length; i++)
        {
            if (_positions.Length > _cards.Length)
            {
                RandomCardIndex = Random.Range(0, _cards.Length);
            }
            else
            {
                do
                {
                    RandomCardIndex = Random.Range(0, _cards.Length);
                } while (cardSlots.Contains(RandomCardIndex));

                cardSlots.Add(RandomCardIndex);
            }

            GameObject newCard = Instantiate(_cards[RandomCardIndex], _positions[i].position, Quaternion.identity, _positions[i]);
            ActiveCards.Add(newCard);
        }
    }

    public void DestroyCards()
    {
        foreach (GameObject card in ActiveCards)
        {
            if (card != null)
            {
                Destroy(card);
            }
        }
        ActiveCards.Clear();
    }
}
