% Morti in combattimento, danno da arma



% E1 - Esperienza attaccante
% E2 - Esperienza difensore
% F1 - Formazione attaccante
% F2 - Formazione difensore
% A - Danno arma attaccante
% D - Difesa armatura difensore
% N - Numero attaccanti
% Hp2 - Vita difensore


function out = MortiArma(E1, E2, F1, F2, A, D, N, HP2 )
    

out = (E1/E2)*(F1/F2)*(A/D)*(N/HP2);
    

end