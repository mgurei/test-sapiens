%Different Type of fight

function out = Death(E1, E2, F1, F2, At, Ac, Ap, Dt, Dc, Dp, N, HP2 )
    

out = (E1/E2)*(F1/F2)*(log(At/Dt)+(log(Ap/Dp))+(Ac/Dc))*(N/HP2);
    

end