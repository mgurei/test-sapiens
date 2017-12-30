Legnati = zeros (30, 20);

for A = 1:30
for D = 1:20
    

        
Legnati(A, D) = MortiArma(.5, .5,1, 1, A, D, 300, 50 );
end
end



%% plot
plot(Legnati)