using UnityEngine;
using UnityEngine.UI;

public class StarRating : MonoBehaviour
{
    [SerializeField] private Image[] _stars; // Array of 3 star images
    private Color _filledStarColor = Color.yellow;
    private Color _emptyStarColor = Color.gray;
    [SerializeField] private Sprite _emptyStarSprite;
    [SerializeField] private Sprite _filledStarSprite;

    /// <summary>
    /// Updates the star display based on the player's score percentage.
    /// 1 star = 30%, 2 stars = 60%, 3 stars = 100%.
    /// </summary>
    public void SetStars(int score, int minimumScore)
    {
        if (_stars.Length != 3)
        {
            Debug.LogError("StarRating must have exactly 3 stars configured!");
            return;
        }

        float percentage = minimumScore > 0 ? (float)score / minimumScore * 100f : 0f;
        int starsToFill = 0;

        if (percentage >= 100f) starsToFill = 3;
        else if (percentage >= 60f) starsToFill = 2;
        else if (percentage >= 30f) starsToFill = 1;

        for (int i = 0; i < _stars.Length; i++)
        {
            _stars[i].sprite = i < starsToFill ? _filledStarSprite : _emptyStarSprite;
        }
    }

    /// <summary>
    /// Resets all stars to empty state.
    /// </summary>
    public void ResetStars()
    {
        foreach (Image star in _stars)
        {
            star.sprite = _emptyStarSprite;
        }
    }
}
