using LaserChess.AI;
using LaserChess.Control;
using LaserChess.Core.Enums;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace LaserChess.State
{
    public class Scenario : MonoBehaviour
    {
        [SerializeField] Button _endTurnBtn;

        private bool _buttonIsClicked;
        private GameObject _projectilesContainer;
        private PlayerController _playerController;
        private AIController _aiController;

        private void Awake()
        {
            this._projectilesContainer = GameObject.Find("ProjectilesContainer");
            this._playerController = GameObject.Find("PlayerPieces").GetComponent<PlayerController>();
            this._aiController = GameObject.Find("AIPieces").GetComponent<AIController>();
        }

        private void Start()
        {
            this._endTurnBtn.onClick.AddListener(() =>
            {
                if (this._playerController.isDisabled) return;
                this._buttonIsClicked = !this._buttonIsClicked;
            });

            this.StartCoroutine(this.ChangeState(States.START));
        }

        private IEnumerator ChangeState(string state)
        {
            switch (state)
            {
                case States.START:
                    {
                        this._playerController.isDisabled = true;

                        yield return this.ChangeState(States.PLAYER_TURN);

                        break;
                    }

                case States.PLAYER_TURN:
                    {
                        this._playerController.isDisabled = false;

                        yield return new WaitUntil(() => this._buttonIsClicked);

                        this._playerController.DestroyMarkers();

                        this._playerController.isDisabled = true;

                        yield return this.ChangeState(States.AI_TURN);

                        break;
                    }

                case States.AI_TURN:
                    {
                        if (this._aiController.PiecesCount == 0) 
                        {
                            yield return this.ChangeState(States.END);
                            break;
                        }

                        this._aiController.MovePieces(PiecesIdsEnum.Drone);
                        this._aiController.Attack(PiecesIdsEnum.Drone);

                        yield return new WaitForSeconds(this._aiController.DelayBetweenPieceTurns);

                        this._aiController.MovePieces(PiecesIdsEnum.Dreadnought);
                        this._aiController.Attack(PiecesIdsEnum.Dreadnought);

                        yield return new WaitForSeconds(this._aiController.DelayBetweenPieceTurns);

                        this._aiController.MovePieces(PiecesIdsEnum.CommandUnit);

                        yield return new WaitUntil(() => this._projectilesContainer.transform.childCount == 0);

                        yield return this.ChangeState(States.RESET);

                        break;
                    }

                case States.RESET:
                    {
                        if (this._aiController.PiecesCount == 0 || this._playerController.PiecesCount == 0)
                        {
                            yield return this.ChangeState(States.END);
                            break;
                        }

                        this._playerController.Reset();
                        this._buttonIsClicked = false;
                        yield return this.ChangeState(States.START);

                        break;
                    }

                case States.END:
                    {
                        Debug.Log("End");
                        // TODO [GM]: Display win screen
                        break;
                    }

                default:
                    break;
            }
        }
    }
}