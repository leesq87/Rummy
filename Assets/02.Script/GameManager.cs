using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public int PlayerNum = 2;

	// Use this for initialization
	void Start () {
		
		CreateTile ();
		ShuffleTile ();
		distribution ();
		Sort_Numbering ();
		Continuous_Number ();
		//Same_Number ();
		ViewTile ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void CreateTile(){
		int number = 1;
		int color = 1;
		bool doubleTile = false;
		for(int i =0; i<106; i++){
			StaticList.Tile _Tile = new StaticList.Tile ();
			if (number == 14) {
				number = 1;
				color++;
			}

			if(!doubleTile && color == 5){
				doubleTile = true;
				color = 1;
			}

			if(color == 5){
				number = 100;
			}

			_Tile.Number = number;
			_Tile.Color = color;

			StaticList.Tiles.Add (_Tile);
			number++;
		}
	}

	void ViewTile(){
		for(int i =0; i<StaticList.Player1.Count; i++){
			Debug.Log ("Player1 " + i + " Number : " + StaticList.Player1[i].Number + ", Tiles Color : " + StaticList.Player1[i].Color + ", Continuous : " + StaticList.Player1[i].Continuous + ", register : " + StaticList.Player1[i].register + ", Start End : " + StaticList.Player1[i].Start + ", " + StaticList.Player1[i].End);
		}

		for(int i =0; i<StaticList.Player2.Count; i++){
			Debug.Log ("Player2 " + i + " Number : " + StaticList.Player2[i].Number + "  Tiles Color : " + StaticList.Player2[i].Color);
		}

		for(int i =0; i<StaticList.Player3.Count; i++){
			Debug.Log ("Player3 " + i + " Number : " + StaticList.Player3[i].Number + "  Tiles Color : " + StaticList.Player3[i].Color);
		}

		for(int i =0; i<StaticList.Player4.Count; i++){
			Debug.Log ("Player4 " + i + " Number : " + StaticList.Player4[i].Number + "  Tiles Color : " + StaticList.Player4[i].Color);
		}

		for(int i =0; i<StaticList.Tiles.Count; i++){
			Debug.Log ("Tiles" + i + " Number : " + StaticList.Tiles[i].Number + "  Tiles Color : " + StaticList.Tiles[i].Color);
		}
	}

	void ShuffleTile(){
		//pass 1
		for (int i = 0; i < StaticList.Tiles.Count-1; i++) {
			int num = Random.Range (i+1 , 106);
			Swap (StaticList.Tiles [i], StaticList.Tiles [num]);
		}
		//pass 2
		for (int i = 0; i < StaticList.Tiles.Count-1; i++) {
			int num = Random.Range (i+1 , 106);
			Swap (StaticList.Tiles [i], StaticList.Tiles [num]);
		}
	}

	void Swap(StaticList.Tile T1, StaticList.Tile T2){
		int temp_number = T1.Number;
		int temp_color = T1.Color;
		T1.Number = T2.Number;
		T1.Color = T2.Color;
		T2.Number = temp_number;
		T2.Color = temp_color;
	}

	void distribution(){
		for(int i = 0; i < PlayerNum * 14; i++){
			if (i < 14) {
				StaticList.Player1.Add (StaticList.Tiles[0]);
			} else if (i < 28) {
				StaticList.Player2.Add (StaticList.Tiles[0]);
			} else if (i < 42) {
				StaticList.Player3.Add (StaticList.Tiles[0]);
			} else {
				StaticList.Player4.Add (StaticList.Tiles[0]);
			}
			StaticList.Tiles.Remove (StaticList.Tiles[0]);
		}
	}

	void Sort_Numbering(){
		//player1
		for(int i =0; i<StaticList.Player1.Count-1; i++){
			for(int j =i; j<StaticList.Player1.Count; j++){
				if(StaticList.Player1[i].Color > StaticList.Player1[j].Color){
					Swap (StaticList.Player1[i], StaticList.Player1[j]);
				}
				else if(StaticList.Player1[i].Color == StaticList.Player1[j].Color){
					if(StaticList.Player1[i].Number > StaticList.Player1[j].Number){
						Swap (StaticList.Player1[i], StaticList.Player1[j]);
					}
				}
			}
		}
	}

	void Continuous_Number(){
		for (int i = 0; i < StaticList.Player1.Count - 1; i++) {
			for (int j = i+1; j < StaticList.Player1.Count; j++) {
				if(StaticList.Player1[i].Number + 1 + StaticList.Player1 [i].Continuous == StaticList.Player1[j].Number && StaticList.Player1[i].Color == StaticList.Player1[j].Color){
					StaticList.Player1 [i].Continuous++;
				}
			}
		}

		int num = 0;
		for(int i = 0; i < StaticList.Player1.Count; i++){
			//처음번호
			if(StaticList.Player1 [i].Continuous >= 2 && num == 0){
				StaticList.Player1 [i].register = true;
				StaticList.Player1 [i].Start = 1;
				num++;
			} 
			//이후번호
			else if(num > 0){
				//같은번호일 경우 끊어서 등록할지 넘길지 판단
				if (i != StaticList.Player1.Count - 1 && StaticList.Player1 [i].Number == StaticList.Player1 [i + 1].Number) {
					// 3개이상 등록했는지
					if (num >= 3) {
						//다음숫자가 첫등록 가능한 숫자일 경우
						if (StaticList.Player1 [i + 1].Continuous >= 2) {
							StaticList.Player1 [i].End = 1;
							num = 0;
						} else {
							//넘기기
						}
					} else {
						//넘기기
					}
				} 
				//다른번호일 경우 등록
				else {
					StaticList.Player1 [i].register = true;
					num++;

					if(StaticList.Player1 [i].Continuous == 0){
						StaticList.Player1 [i].End = 1;
						num = 0;
					}
				}
			}
		}
	}

	void Same_Number(){
		StaticList.Check _Check = new StaticList.Check ();
		_Check.Number = 0;
		_Check.TF = false;
		StaticList.Checks.Add (_Check);
		StaticList.Checks.Add (_Check);
		StaticList.Checks.Add (_Check);
		StaticList.Checks.Add (_Check);



		for (int i = 0; i < StaticList.Player1.Count - 2; i++) {
			
			//등록 가능하지 않은 타일 중
			if(!StaticList.Player1 [i].register){
				StaticList.Checks [StaticList.Player1 [i].Color - 1].TF = true;
				StaticList.Checks [StaticList.Player1 [i].Color - 1].Number = i;

				for (int j = i + 1; j < StaticList.Player1.Count; j++) {
					if(StaticList.Player1 [i].Number == StaticList.Player1 [j].Number){
						if(!StaticList.Checks [StaticList.Player1 [j].Color-1].TF){
							StaticList.Checks [StaticList.Player1 [j].Color-1].Number = j;
							StaticList.Checks [StaticList.Player1 [j].Color-1].TF = true;
						}
					}
				}

				//같은 숫자 3개이상일 경우
				int num = 0;
				for(int m = 0; m < StaticList.Checks.Count; m++){
					if(StaticList.Checks[m].TF){
						num++;
					}
				}

				if(num >= 3){
					Debug.Log ("num : " + num);
					for(int k = 0; k < 4; k++){
						if(StaticList.Checks[k].TF){
							StaticList.Player1 [StaticList.Checks [k].Number].register = true;
						}
					}
				}

				for(int k = 0; k < 4; k++){
					if(StaticList.Checks[k].TF){
						StaticList.Checks [k].TF = false;
						StaticList.Checks [k].Number = 0;
					}
				}
			}

		}
	}
}
