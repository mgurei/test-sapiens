% Morti in combattimento, danno da arma



% E1 - Esperienza attaccante
% E2 - Esperienza difensore
% F1 - Formazione attaccante
% F2 - Formazione difensore
% F - F1/F2
% A - Danno arma attaccante
% D - Difesa armatura difensore
% D - A/D
% N - Numero attaccanti
% Hp2 - Vita difensore


function out = MortiArma2(E1, E2, F, D, N, HP2 )
    

out = (E1/E2)*(F)*(D)*(N/HP2);
    

end