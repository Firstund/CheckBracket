# CheckBracket
이 코드는 입력될 구문에 포함된 모든 괄호들이 아래 3가지 고전에 부합하는지 체크하는 알고리즘입니다.
### 조건1
여는 괄호와 닫는 괄호의 수가 같은가?
(ex: "A[(i+2)=3;" -> 조건 1 위반)
### 조건2
각 괄호의 여는 괄호가 닫는 괄호보다 먼저 나오는가?
(ex: "A}i+1{" -> 조건 2 위반)
### 조건3
각 괄호가 서로 다른 괄호와 교차해서 쓰이지는 않았는가?
(ex: "A[(i+1])=0;" -> 조건 3 위반)
